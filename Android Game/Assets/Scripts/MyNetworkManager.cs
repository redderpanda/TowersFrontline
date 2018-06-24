using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;


public class MyNetworkManager : NetworkManager {
    private float nextRefreshTime;

    public void StartHosting()
    {
        StartMatchMaker();
        matchMaker.CreateMatch("Match", 2, true, "", "", "", 0, 0, OnMatchCreated);
        //base.StartHost();
    }
    private void OnMatchCreated(bool success, string extendedinfo, MatchInfo responsedata)
    {
        base.StartHost(responsedata);
        RefreshMatches();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Time.time >= nextRefreshTime)
                RefreshMatches();
        }
    }

    private void RefreshMatches()
    {
        nextRefreshTime = Time.time + 5f;
        if(matchMaker == null)
        {
            StartMatchMaker();
        }
        matchMaker.ListMatches(0, 10, "",true, 0,0,HandleListMatchesComplete);
    }

    private void HandleListMatchesComplete(bool success, string extendedinfo, List<MatchInfoSnapshot> responsedata)
    {
        //throw new System.NotImplementedException();
        AvailableMatchList.HandleNewMatchList(responsedata);
    }

    public void JoinMatch(MatchInfoSnapshot match)
    {
        if (matchMaker == null)
            StartMatchMaker();

        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, HandleJoinedMatch);
    }

    private void HandleJoinedMatch(bool success, string extendedinfo, MatchInfo responsedata)
    {
        StartClient(responsedata);
    }



}
