using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Economy : NetworkBehaviour {

    public float current_energy;
    public float multiplier;
    public Text text_object;

    // Use this for initialization
    void Start() {
        if (isLocalPlayer)
        {
            text_object = GetComponentInChildren<Text>();
            current_energy = 0f;
            multiplier = 100f;
        }
        else
        {
			Debug.Log ("Mac Is Dumb");
            enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        current_energy += (1 * multiplier) * Time.deltaTime;
        if(isLocalPlayer)
            text_object.text = current_energy.ToString("c2");
        // "d2" = 111.11
	}
}
