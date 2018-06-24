using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExpansion : MonoBehaviour {
    public GameObject object1;
    public Material redmat,bluemat;
    public bool checker;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (checker == true)
            object1 = null;
        if (object1.CompareTag("No_Tower_2")) {
            this.GetComponent<MeshRenderer>().material = redmat;
            this.tag = "RedArea";
        }

        if (object1.CompareTag("No_Tower_1")) {
            this.GetComponent<MeshRenderer>().material = bluemat;
            this.tag = "BlueArea";
        }
	}
}
