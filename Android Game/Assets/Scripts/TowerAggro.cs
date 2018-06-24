using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TowerAggro : NetworkBehaviour {
    public TowerScript this_tower;

	// Use this for initialization
	void Start () {
        this_tower = GetComponentInParent<TowerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
            Debug.Log("trigger");
        if (other.gameObject.GetComponent<SpawnableGuy>() != null)
        {
            Debug.Log("came in range");
            this_tower.targets_in_range.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SpawnableGuy>() != null)
        {
            this_tower.targets_in_range.Remove(other.gameObject);
        }
    }

    [Command]
    public void CmdTrigger()
    {
        Debug.Log("triggered by something");
    }
}
