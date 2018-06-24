using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Jared_Spawn_Script : NetworkBehaviour {
    public Transform spawnPoint;

////////////////// SPAWNABLE OBJECTS  ////////////////////
    public GameObject object1; //ENEMY_1
	public GameObject object2; //ENEMY_2
    public GameObject object3; //TOWER_1
    public GameObject object4; //TOWER_2
    public GameObject object5; //ECON_1
    public GameObject object6; //ECON_2
    public GameObject object7; //SHIELD_1
    public GameObject object8; //SHIELD_2
    public GameObject object9; //SNIPER_1
    public GameObject object10; //SNIPER_2

///////////////// BUTTONS  //////////////////////////
    public Toggle basicWarrior;
    public Toggle basicTower;
    public Toggle econTower;
    public Toggle ShieldWarrior;
    public Toggle Sniper;
    public Economy econ;
 
////////////////  BOOLS  ////////////////////////////    
    public bool warrior_on;
    public bool tower_on;
    public bool econ_tower_on;
    public bool shield_unit_on;
    public bool sniper_on;


    public void Start()
    {
        if (isLocalPlayer)
        {
            econ = GetComponent<Economy>();
        }
        if(!isLocalPlayer)
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }
        warrior_on = false;
        tower_on = false;
        econ_tower_on = false;
        shield_unit_on = false;
        sniper_on = false;
        Component[] toggles;
        toggles = GetComponentsInChildren<Toggle>();
        basicWarrior = toggles[0].GetComponent<Toggle>();
        basicTower = toggles[1].GetComponent<Toggle>();
        econTower = toggles[2].GetComponent<Toggle>();
        ShieldWarrior = toggles[3].GetComponent<Toggle>();
        Sniper = toggles[4].GetComponent<Toggle>();
        //basicWarrior = GetComponentInChildren<Toggle>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        basicWarrior.onValueChanged.AddListener(delegate { WarriorPressed(); });
        basicTower.onValueChanged.AddListener(delegate { TowerPressed(); });
        econTower.onValueChanged.AddListener(delegate { EconTowerPressed(); });
        ShieldWarrior.onValueChanged.AddListener(delegate { ShieldPressed(); });
        Sniper.onValueChanged.AddListener(delegate { SniperPressed(); });
    }


    public void Update()
    {
        if (!isLocalPlayer)
        { return; }


        //Debug.Log("warrior toggled");
        Touch touch;
        for (int i = 0; i < Input.touches.Length; i++)
        {
            touch = Input.touches[i];
            if (touch.phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                RaycastHit rayHit;
                //var ray = Camera.main.ScreenPointToRay (touch.position);
                if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out rayHit, 100))
                {
                    if (warrior_on)
                    {
                        if (econ.current_energy >= 300)
                        {
                            if (isServer && rayHit.collider.tag == "BlueArea")
                            {
                                CmdSpawnMyGuy(rayHit.point, "Enemy_1");
                                econ.current_energy -= 300f;
                            }
                            else
                            {
                                if (!isServer && rayHit.collider.tag == "RedArea")
                                {
                                    CmdSpawnMyGuy(rayHit.point, "Enemy_2");
                                    econ.current_energy -= 300f;
                                }
                            }

                        }

                    }

                    else if (rayHit.collider != null)
                    {
                        if (tower_on)
                        {
                            if (rayHit.transform.gameObject.tag == "No_Tower_1" && econ.current_energy >= 1000 && isServer)
                            {
                                CmdTouchEmptyTower(rayHit.point, rayHit.transform.gameObject, "Tower_1");
                                econ.current_energy -= 1000f;
                            }
                            else if (rayHit.transform.gameObject.tag == "No_Tower_2" && econ.current_energy >= 1000 && !isServer)
                            {
                                CmdTouchEmptyTower(rayHit.point, rayHit.transform.gameObject, "Tower_2");
                                econ.current_energy -= 1000f;
                            }
                        }

                        else if (econ_tower_on)
                        {
                            if (rayHit.transform.gameObject.tag == "No_Tower_1" && econ.current_energy >= 500 && isServer)
                            {
                                CmdTouchEmptyTower(rayHit.point, rayHit.transform.gameObject, "Econ_Tower_1");
                                econ.current_energy -= 500f;
                                econ.multiplier *= 1.5f;
                            }
                            else if (rayHit.transform.gameObject.tag == "No_Tower_2" && econ.current_energy >= 500 && !isServer)
                            {
                                CmdTouchEmptyTower(rayHit.point, rayHit.transform.gameObject, "Econ_Tower_2");
                                econ.current_energy -= 500f;
                                econ.multiplier *= 1.5f;
                            }
                        }

                        else if (shield_unit_on)
                        {
                            if (econ.current_energy >= 500)
                            {
                                if (isServer && rayHit.collider.tag == "BlueArea")
                                {
                                    CmdSpawnMyGuy(rayHit.point, "Shield_1");
                                    econ.current_energy -= 500f;
                                }
                                else
                                {
                                    if (!isServer && rayHit.collider.tag == "RedArea")
                                    {
                                        CmdSpawnMyGuy(rayHit.point, "Shield_2");
                                        econ.current_energy -= 500f;
                                    }
                                }

                            }

                        }
                        else if (sniper_on)
                        {
                            if (econ.current_energy >= 200)
                            {
                                if (isServer && rayHit.collider.tag == "BlueArea")
                                {
                                    CmdSpawnMyGuy(rayHit.point, "Sniper_1");
                                    econ.current_energy -= 200f;
                                }
                                else
                                {
                                    if (!isServer && rayHit.collider.tag == "RedArea")
                                    {
                                        CmdSpawnMyGuy(rayHit.point, "Sniper_2");
                                        econ.current_energy -= 200f;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    Vector3 click_spawn_point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            //    CmdSpawnMyGuy(click_spawn_point, "Enemy_1");
            //}

            //if (Input.GetMouseButtonDown(1))
            //{
            //    Vector3 click_spawn_point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            //    CmdSpawnMyGuy(click_spawn_point, "Enemy_2");
            //}
        }
    }

    [Command]
    public void CmdTouchEmptyTower(Vector3 point, GameObject go, string spawn_type)
    {
        if (spawn_type.Equals("Tower_1"))
        {
            go.SetActive(false);
            //Destroy(go);
           // NetworkServer.Destroy(go);
           // Debug.Log("touched a empty tower space");
            GameObject GO = Instantiate(object3, point, transform.rotation);
            GO.GetComponent<TowerScript>().NoTowerPlot = go;
            NetworkServer.Spawn(GO);
            RpcTouchEmptyTower(go, GO);
            
        }
        else if (spawn_type.Equals("Tower_2"))
        {
            go.SetActive(false);
            //Destroy(go);
            //NetworkServer.Destroy(go);
            //Debug.Log("touched a empty tower space");
            GameObject GO = Instantiate(object4, point, transform.rotation);
            GO.GetComponent<TowerScript>().NoTowerPlot = go;
            NetworkServer.Spawn(GO);
            RpcTouchEmptyTower(go, GO);

        }
        else if(spawn_type.Equals("Econ_Tower_1"))
        {
            go.SetActive(false);
            //Destroy(go);
            //NetworkServer.Destroy(go);
            GameObject GO = Instantiate(object5, point, transform.rotation);
            GO.GetComponent<EconomyTower>().my_economy = econ;
            GO.GetComponent<EconomyTower>().NoTowerPlot = go;
            NetworkServer.Spawn(GO);
            RpcTouchEmptyTower(go, GO);

        }
        else if (spawn_type.Equals("Econ_Tower_2"))
        {
            go.SetActive(false);
            //Destroy(go);
            //NetworkServer.Destroy(go);
            GameObject GO = Instantiate(object6, point, transform.rotation);
            GO.GetComponent<EconomyTower>().my_economy = econ;
            GO.GetComponent<EconomyTower>().NoTowerPlot = go;
            NetworkServer.Spawn(GO);
            RpcTouchEmptyTower(go, GO);

        }
    }

    [ClientRpc]
    void RpcTouchEmptyTower(GameObject plot, GameObject tower)
    {
        Debug.Log("turnig plot off");
        if(tower.GetComponent<EconomyTower>() != null)
            tower.GetComponent<EconomyTower>().NoTowerPlot = plot;
        else if (tower.GetComponent<TowerScript>() != null)
            tower.GetComponent<TowerScript>().NoTowerPlot = plot;
        plot.SetActive(false);
    }

    [Command]
	void CmdSpawnMyGuy(Vector3 point, string spawn_type)
    {
		//Economy econ = player.GetComponent<Economy> ();


		if(spawn_type.Equals("Enemy_1")){
			GameObject go = Instantiate(object1, point, transform.rotation);
			NetworkServer.Spawn(go);
			//Destroy(go, 2);
		}
		else if(spawn_type.Equals("Enemy_2")){
			GameObject go = Instantiate (object2, point, transform.rotation);
			NetworkServer.Spawn(go);
			//Destroy(go, 2);
		}
        else if (spawn_type.Equals("Shield_1"))
        {
            GameObject go = Instantiate(object7, point, transform.rotation);
            NetworkServer.Spawn(go);
            //Destroy(go, 2);
        }
        else if (spawn_type.Equals("Shield_2"))
        {
            GameObject go = Instantiate(object8, point, transform.rotation);
            NetworkServer.Spawn(go);
            //Destroy(go, 2);
        }
        else if (spawn_type.Equals("Sniper_1"))
        {
            GameObject go = Instantiate(object9, point, transform.rotation);
            NetworkServer.Spawn(go);
            //Destroy(go, 2);
        }
        else if (spawn_type.Equals("Sniper_2"))
        {
            GameObject go = Instantiate(object10, point, transform.rotation);
            NetworkServer.Spawn(go);
            //Destroy(go, 2);
        }


        //econ.current_energy -= 1f;
        //RpcSpawnMyGuy (player);
    }
    

	[ClientRpc]
	void RpcSpawnMyGuy(GameObject player)
	{
		Economy econ = player.GetComponent<Economy> ();
		econ.current_energy -= 1f;
	}

    public void WarriorPressed()
    {

        if (tower_on)
        {
            //tower_on = !tower_on;
            basicTower.isOn = false;
        }
        if(econ_tower_on)
        {
            //econ_tower_on = !econ_tower_on;
            econTower.isOn = false;
        }
        if (shield_unit_on)
        {
            ShieldWarrior.isOn = false;
        }
        if (sniper_on)
        {
            Sniper.isOn = false;
        }
        warrior_on = !warrior_on;
    }

    public void TowerPressed()
    {

        if (econ_tower_on)
        {
            //econ_tower_on = !econ_tower_on;
            econTower.isOn = false;
        }
        if(warrior_on)
        {
            //warrior_on = !warrior_on;
            basicWarrior.isOn = false;
        }
        if (shield_unit_on)
        {
            ShieldWarrior.isOn = false;
        }
        if (sniper_on)
        {
            Sniper.isOn = false;
        }
        tower_on = !tower_on;

    }

    public void EconTowerPressed()
    {
        if (tower_on)
        {
            //tower_on = !tower_on;
            basicTower.isOn = false;
        }
        if (warrior_on)
        {
            //warrior_on = !warrior_on;
            basicWarrior.isOn = false;
        }
        if (shield_unit_on)
        {
            ShieldWarrior.isOn = false;
        }
        if (sniper_on)
        {
            Sniper.isOn = false;
        }
        econ_tower_on = !econ_tower_on;
    }

    public void ShieldPressed()
    {
        if (tower_on)
        {
            basicTower.isOn = false;
        }
        if (warrior_on)
        {
            basicWarrior.isOn = false;
        }
        if (econ_tower_on)
        {
            econTower.isOn = false;
        }
        if (sniper_on)
        {
            Sniper.isOn = false;
        }
        shield_unit_on = !shield_unit_on;
    }

    public void SniperPressed()
    {
        if (warrior_on)
        {
            basicWarrior.isOn = false;
        }
        if (tower_on)
        {
            basicTower.isOn = false;
        }
        if (econ_tower_on)
        {
            econTower.isOn = false;
        }
        if (shield_unit_on)
        {
            ShieldWarrior.isOn = false;
        }
        sniper_on = !sniper_on;
    }


}
