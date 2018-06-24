using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.Networking.Match;

public class MatchListPanel : MonoBehaviour {

    [SerializeField]
    private JoinGame joinButtonPrefab;

	// Use this for initialization
	private void Awake () {
        AvailableMatchList.OnAvailableMatchesChanged += AvailableMatchList_OnAvailableMatchesChanged;
	}
	
    private void AvailableMatchList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches)
    {
        ClearExistingButtons();
        CreateNewJoinGameButtons(matches);
    }
    
	private void ClearExistingButtons()
    { 
        var buttons = GetComponentsInChildren<JoinGame>();
        foreach(var button in buttons)
        {
            Destroy(button.gameObject);
        }
	}

    private void CreateNewJoinGameButtons(List<MatchInfoSnapshot> matches)
    {
        foreach(var match in matches)
        {
            var button = Instantiate(joinButtonPrefab);
            button.Initialize(match, transform);
        }
    }


}
