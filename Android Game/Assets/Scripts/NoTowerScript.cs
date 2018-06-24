using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NoTowerScript : NetworkBehaviour {
    public int maxHealth;
    public int currentHealth;
    public Material redmat, bluemat;

    // Use this for initialization
    void Start() {
        maxHealth = 50;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        if (currentHealth <= 0 && this.CompareTag("No_Tower_1"))
        {
            StartCoroutine(ChangeColor());
        }
        if (currentHealth <= 0 && this.CompareTag("No_Tower_2"))
        {
            StartCoroutine(ChangeColor2());
        }
    }

    [Command]
    void CmdChangeColor()
    {
        this.GetComponent<MeshRenderer>().material = redmat;
        this.tag = "No_Tower_2";
        currentHealth = maxHealth;
        Rpc_ChangeColor();
    }

    [ClientRpc]
    void Rpc_ChangeColor()
    {
        if (!isServer)
        {
            this.GetComponent<MeshRenderer>().material = redmat;
            this.tag = "No_Tower_2";
            currentHealth = maxHealth;
        }
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(.5f);
        CmdChangeColor();
    }



/// ///////////////////////////////////////////////////////////////////
    [Command]
    void CmdChangeColor2()
    {
        this.GetComponent<MeshRenderer>().material = bluemat;
        this.tag = "No_Tower_1";
        currentHealth = maxHealth;
        Rpc_ChangeColor2();
    }

    [ClientRpc]
    void Rpc_ChangeColor2()
    {
        if (!isServer) {
            this.GetComponent<MeshRenderer>().material = bluemat;
            this.tag = "No_Tower_1";
            currentHealth = maxHealth;
        }    
    }

    IEnumerator ChangeColor2()
    {
        yield return new WaitForSeconds(.5f);
        CmdChangeColor2();
    }

    
}
