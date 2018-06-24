using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Aggro_Range : MonoBehaviour {
	private Better_AI unit;
	public GameObject base_unit;
	public bool is_attacking;
	private List<GameObject> Things_To_Remove;

//	public string enemy_unit_tag;
//	public string enemy_tower_tag;
//	public string enemy_plot_tag;

	//[System.Serializable]
	public List<string> Enemy_Tags;

	private List<GameObject> Enemies_in_range;

	// Use this for initialization
	void Start () {
		//Possibly Change This To A Public Variable Assigning Instead To Be More Efficient
		unit = base_unit.GetComponent<Better_AI> ();
		Enemies_in_range = new List<GameObject>();
		Things_To_Remove = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.tag != "Dead") {

			transform.position = base_unit.transform.position;
			//Check If Enemy In List Is Dead


			foreach (GameObject enemy_unit in Enemies_in_range) {
				if (!health_not_zero(enemy_unit)) {
					//Enemies_in_range.Remove (enemy_unit);
					Things_To_Remove.Add (enemy_unit);
				}
			}

			foreach (GameObject enemy in Things_To_Remove) {
				Enemies_in_range.Remove (enemy);
			}

			Things_To_Remove.Clear ();
		} else {
			//GameObject Parent = this.gameObject.transform.parent.gameObject;
			//Destroy (Parent, 1f);
			//Destroy (this.gameObject, 1f);
		}
	}

	void OnTriggerEnter(Collider _collider){
		
		//Add To List Of Enemies In Range and Target if Currently Targetting Base
		//Debug.Log("Trigger Entered" + _collider.gameObject.tag);
		string col_tag = _collider.tag;



		if (Enemy_Tags.Contains (col_tag)) {

            if (_collider.GetComponent<SpawnableGuy>())
            {
                if (_collider.GetComponent<SpawnableGuy>().current_health > 0)
                {
                    if (unit.Current_Target == unit.Enemy_Base)
                    {
                        //If Currently Targeting Enemy_Base Default Target
                        Enemies_in_range.Add(_collider.gameObject);
                        unit.Current_Target = _collider.gameObject;
                    }
                    else
                    {
                        //If Targetting Something Else
                        Enemies_in_range.Add(_collider.gameObject);
                    }
                }
            }
            else if (_collider.GetComponent<TowerScript>())
            {
                if (_collider.GetComponent<TowerScript>().currenthealth > 0)
                {
                    if (unit.Current_Target == unit.Enemy_Base)
                    {
                        //If Currently Targeting Enemy_Base Default Target
                        Enemies_in_range.Add(_collider.gameObject);
                        unit.Current_Target = _collider.gameObject;
                    }
                    else
                    {
                        //If Targetting Something Else
                        Enemies_in_range.Add(_collider.gameObject);
                    }
                }
            }
            else if (_collider.GetComponent<NoTowerScript>())
            {
                if (_collider.GetComponent<NoTowerScript>().currentHealth > 0)
                {
                    if (unit.Current_Target == unit.Enemy_Base)
                    {
                        //If Currently Targeting Enemy_Base Default Target
                        Enemies_in_range.Add(_collider.gameObject);
                        unit.Current_Target = _collider.gameObject;
                    }
                    else
                    {
                        //If Targetting Something Else
                        Enemies_in_range.Add(_collider.gameObject);
                    }
                }
            }
            else if (_collider.GetComponent<BaseScript>())
            {
                if (_collider.GetComponent<BaseScript>().current_health > 0)
                {
                    if (unit.Current_Target == unit.Enemy_Base)
                    {
                        //If Currently Targeting Enemy_Base Default Target
                        Enemies_in_range.Add(_collider.gameObject);
                        unit.Current_Target = _collider.gameObject;
                    }
                    else
                    {
                        //If Targetting Something Else
                        Enemies_in_range.Add(_collider.gameObject);
                    }
                }
            }


		}
	}


	void OnTriggerExit(Collider _collider){

		if (_collider.gameObject == unit.Current_Target) {
			//If current target leaves aggro range target new unit
			get_new_target ();
		} else {
			if (Enemies_in_range.Contains (_collider.gameObject)) {
				Enemies_in_range.Remove (_collider.gameObject);
			}
		}

	}


	public void get_new_target(){
		if (Enemies_in_range.Count == 0) {
			//if no enemies in range target base
			unit.Current_Target = unit.Enemy_Base;
		} else {
			//if enemies in list target first in list
			foreach(GameObject enemy_unit in Enemies_in_range){
				if (health_not_zero(enemy_unit)) {
					unit.Current_Target = enemy_unit;
					return;
				}
			}
			unit.Current_Target = unit.Enemy_Base;

		}
	}


    public bool health_not_zero(GameObject _object) {
        if (_object.GetComponent<SpawnableGuy>())
        {
            if (_object.GetComponent<SpawnableGuy>().current_health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (_object.GetComponent<TowerScript>())
        {
            if (_object.GetComponent<TowerScript>().currenthealth > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (_object.GetComponent<NoTowerScript>())
        {
            if (_object.GetComponent<NoTowerScript>().currentHealth > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (_object.GetComponent<BaseScript>())
        {
            if (_object.GetComponent<BaseScript>().current_health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}
