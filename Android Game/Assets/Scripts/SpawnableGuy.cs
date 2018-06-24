using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnableGuy : NetworkBehaviour {
    public float current_health;
    public float max_health;
    public float attack_damage;
    public bool dead;
    public float timer;

	// Use this for initialization
	void Start () {
        //dead = false;
        current_health = max_health;
	}
	
	// Update is called once per frame
	void Update () {
        OnDeath();
	}
    public void OnDeath()
    {
        if(current_health <= 0)
        {
            current_health = 0;
            if (!dead)
            {
				this.gameObject.tag = "Dead";
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(NetDestroy());
                //Destroy(this.gameObject, 1f);
                //dead = true;
            }
        }
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
