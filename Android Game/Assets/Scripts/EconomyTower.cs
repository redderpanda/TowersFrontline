using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EconomyTower : NetworkBehaviour {
    public Economy my_economy;
    public bool dead;
    public GameObject NoTowerPlot;

    // Use this for initialization
    //void Awake () {
    //       my_economy.multiplier *= 2;
    //}

    //private void OnDestroy()
    //{
    //    my_economy.multiplier /= 2;
    //}
    public void Awake()
    {
        dead = false;
    }
    public void Update()
    {
        if (this.gameObject.tag == "Dead")
        {
            if (!dead)
            {
                CmdReactivate();
                my_economy.multiplier /= 2;
                dead = true;
            }
        }
    }

    [Command]
    public void CmdReactivate()
    {
        NoTowerPlot.SetActive(true);
        Rpc_Reactivate();
    }

    [ClientRpc]
    public void Rpc_Reactivate()
    {
        Debug.Log("RPC GETTING CALLED");
        if(!isServer)
            NoTowerPlot.SetActive(true);
    }

}
