using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TowerScript : NetworkBehaviour {
    public List<GameObject> targets_in_range;
    public float damage;
    public float multiplier;
    public int maxhealth, currenthealth;
    public GameObject NoTowerPlot;
    public Material redmat, bluemat;
	public bool dead;

	// Use this for initialization
	void Start () {
        damage = 10f;
        multiplier = 1f;
        targets_in_range = new List<GameObject>();
        maxhealth = 100;
        currenthealth = maxhealth;
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(targets_in_range.Count > 0)
        {
            foreach(GameObject target in targets_in_range)
            {
                if(target.GetComponent<SpawnableGuy>() != null)
                {
                    SpawnableGuy spawn_object = target.GetComponent<SpawnableGuy>();
                    if (spawn_object.current_health <= 0)
                    {
                        targets_in_range.Remove(target);
                        //Network.Destroy(target);
                        
                    }
                    else
                    {
                        if (this.CompareTag("Tower_1"))
                        {
                            if (spawn_object.CompareTag("Enemy_2"))
                                spawn_object.current_health -= (damage * multiplier) * Time.deltaTime;
                        }
                        if (this.CompareTag("Tower_2"))
                        {
                            if (spawn_object.CompareTag("Enemy_1"))
                                spawn_object.current_health -= (damage * multiplier) * Time.deltaTime;
                        }
                    }
                }
            }
        }
        if(currenthealth <= 0)
        {
			if (!dead) 
			{
				CmdReactivate ();
				StartCoroutine (NetDestroy());
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

	public IEnumerator NetDestroy()
	{
		yield return new WaitForSeconds(1f);
		if (this.gameObject.transform.parent != null)
		{
			GameObject parent = this.gameObject.transform.parent.gameObject;
			NetworkServer.Destroy(parent);
		}
		else
		{
			NetworkServer.Destroy(this.gameObject);
		}

	}
}
