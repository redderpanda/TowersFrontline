using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class NextLevel : MonoBehaviour {
    public string next_scene;
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (this.gameObject.GetComponent<BaseScript>().current_health <= 0)
            ChangeScene(next_scene);
    }

    public void ChangeScene(string scenetoload)
    {
        NetworkManager.singleton.ServerChangeScene(scenetoload);
    }
}
