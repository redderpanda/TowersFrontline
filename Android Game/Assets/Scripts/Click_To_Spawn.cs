using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_To_Spawn : MonoBehaviour {

	public GameObject Base_Unit;
	public bool spawn_unit;
	public Button enable_spawn_button;

	// Use this for initialization
	void Start () {
		spawn_unit = false;
		enable_spawn_button.onClick.AddListener (Toggle_Spawn);
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {

		if (spawn_unit) {
			spawn_on_touch ();
		}


	}

	void spawn_on_touch(){
		Touch touch;
		for (int i = 0; i < Input.touches.Length; i++) {
			touch = Input.touches [i];
			if (touch.phase == TouchPhase.Began) {
				// Construct a ray from the current touch coordinates
				RaycastHit rayHit;
				//var ray = Camera.main.ScreenPointToRay (touch.position);
				if (Physics.Raycast (Camera.main.ScreenPointToRay (touch.position), out rayHit, 100)) {
					Instantiate (Base_Unit, rayHit.point, transform.rotation);
				}
			}
		}
	}

	void Toggle_Spawn(){
		spawn_unit = !spawn_unit;
	}




}
