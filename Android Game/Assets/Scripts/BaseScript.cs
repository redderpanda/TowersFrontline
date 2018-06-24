using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour {
    public float current_health;
    public float max_health;
    public float multiplier;

	// Use this for initialization
	void Start () {
        multiplier = 2f;
        current_health = max_health;
	}
	
	// Update is called once per frame
	void Update () {
        if(current_health > 0)
            IncreaseHealth();
        else
            OnDeath();
	}

    public void IncreaseHealth()
    {
        if(current_health < max_health)
        {
            current_health += (1 * multiplier) * Time.deltaTime;
        }
    }

    public void OnDeath()
    {
        if (current_health <= 0)
        {
            current_health = 0;
            Debug.Log("You Lost");
        }
    }
}
