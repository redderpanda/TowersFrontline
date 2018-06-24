using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit_Mover : MonoBehaviour {

	NavMeshAgent agent;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update() {
		Touch touch;
		for (int i = 0; i < Input.touches.Length; i++) {
			touch = Input.touches [i];
			if (touch.phase == TouchPhase.Began) {
				// Construct a ray from the current touch coordinates
				RaycastHit rayHit;
				//var ray = Camera.main.ScreenPointToRay (touch.position);
				if (Physics.Raycast (Camera.main.ScreenPointToRay (touch.position), out rayHit, 100)) {
					agent.destination = rayHit.point;
				}
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				agent.destination = hit.point;
			}
		}
	}


}
