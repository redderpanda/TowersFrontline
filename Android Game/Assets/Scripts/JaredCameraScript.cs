using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaredCameraScript : MonoBehaviour {

    public float dragSpeed = 1.5f;
    private Vector3 dragOrigin;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        transform.Translate(-move, Space.World);
        //transform.position = new Vector3(Mathf.Clamp(Time.time,-10f,10f), 10f, Mathf.Clamp(Time.time, -5f, 5f));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), 10f, Mathf.Clamp(transform.position.z, -5f, 5f));
    }
}
