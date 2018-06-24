using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Better_AI : NetworkBehaviour {
	public GameObject Enemy_Base;
	public GameObject Aggro_Object;
	private NavMeshAgent Nav_Agent;
	private SpawnableGuy Spawn_Guy;
	public GameObject Current_Target;
	private Aggro_Range aggro_script;
	public float attack_in_range;
	public float attack_speed;
	public bool can_attack;
	public float attack_damage;
	public string Target_Base_Tag;
    public float dist_from;
    private Vector3 ray_start_position;
	public float hit_ray_length = 2f;
	public GameObject hit_Indicator;
	public float indicator_time = 0.5f;
	public float hit_rotate_adjust_y = 0f;
    //LayerMask l_everything;
    //RaycastHit[] things_hit;

	// Use this for initialization
	void Start () {
		Spawn_Guy = gameObject.GetComponent<SpawnableGuy> ();
		Enemy_Base = GameObject.FindGameObjectWithTag (Target_Base_Tag);
		Nav_Agent = gameObject.GetComponent<NavMeshAgent> ();
		aggro_script = Aggro_Object.GetComponent<Aggro_Range> ();
		can_attack = true;
		attack_damage = Spawn_Guy.attack_damage;

        
		Debug.Log ("Getting to HERE");
        if (aggro_script.Enemy_Tags.Contains("Base_1"))
        {
            Enemy_Base = GameObject.FindGameObjectWithTag("Base_1");
        } else
        {
			Debug.Log ("Base_2");
            Enemy_Base = GameObject.FindGameObjectWithTag("Base_2");
        }
            
        

        
        Current_Target = Enemy_Base;
        

        //l_everything = ~0;

        
	}
	
	// Update is called once per frame
	void Update () {
        
        

        if (this.gameObject.tag != "Dead") {
			Nav_Agent.destination = Current_Target.transform.position;

            if (Current_Target.GetComponent<SpawnableGuy>())
            {
                if (Current_Target.GetComponent<SpawnableGuy>().current_health <= 0)
                {
                    aggro_script.get_new_target();
                }
            }
            else if (Current_Target.GetComponent<TowerScript>())
            {
                if (Current_Target.GetComponent<TowerScript>().currenthealth <= 0)
                {
                    aggro_script.get_new_target();
                }
            }
            else if (Current_Target.GetComponent<NoTowerScript>())
            {
                if (Current_Target.GetComponent<NoTowerScript>().currentHealth <= 0)
                {
                    aggro_script.get_new_target();
                }
            }
            else if (Current_Target.GetComponent<BaseScript>())
            {
                if (Current_Target.GetComponent<BaseScript>().current_health <= 0)
                {
                    aggro_script.get_new_target();
                }
            }

            
            //RaycastHit hit;
            if (Vector3.Distance(this.gameObject.transform.position, Current_Target.transform.position) <= attack_in_range)
            {
                Debug.Log("In Range");
                Vector3 fwd = gameObject.transform.TransformDirection(Vector3.forward);
                
                transform.LookAt(Current_Target.transform.position);
                ray_start_position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				Debug.DrawRay(ray_start_position, fwd * hit_ray_length, Color.green);
				RaycastHit[] things_hit = Physics.RaycastAll(ray_start_position, fwd, hit_ray_length);
				if (Current_Target != null) {
					foreach (RaycastHit hit in things_hit)
					{
						if (hit.transform.gameObject != null && hit.rigidbody.gameObject != null && hit.rigidbody.gameObject == Current_Target)
						{
							Debug.Log("Hit The Guy");
							StartCoroutine(Attack(Current_Target));

						}
					}
				}
            }

            //dist_from = Vector3.Distance(this.gameObject.transform.position, Current_Target.transform.position);
            //if (Vector3.Distance(this.gameObject.transform.position, Current_Target.transform.position) <= attack_in_range)
            //{
            //    //Needs to be distance - mesh width
            //    Debug.Log("In Range Of Attack");
            //    StartCoroutine(Attack(Current_Target));
            //}

        } else {
			Aggro_Object.tag = "Dead";
		}
	}
		
	private IEnumerator Attack(GameObject Target){
		if (can_attack) {
            Debug.Log("Attempting To Attack");
			can_attack = false;
            if (Target.GetComponent<SpawnableGuy>())
            {
                Target.GetComponent<SpawnableGuy>().current_health -= attack_damage;
            }
            else if (Target.GetComponent<TowerScript>())
            {
                Target.GetComponent<TowerScript>().currenthealth -= (int)attack_damage;
            }
            else if (Target.GetComponent<NoTowerScript>())
            {
                //Target.GetComponent<NoTowerScript>().currentHealth -= (int)attack_damage;
                Target.GetComponent<NoTowerScript>().currentHealth -= 10;
                Debug.Log("ATTACKED NO TOWER");
            }
            else if (Target.GetComponent<BaseScript>())
            {
                Target.GetComponent<BaseScript>().current_health -= (int)attack_damage;
            }
			if (Target.GetComponent<Better_AI> ()) {
				Better_AI target_unit = Target.GetComponent<Better_AI> ();
				target_unit.Current_Target = this.gameObject;
			}
			StartCoroutine (Spawn_Hit_Indicator(Target));
			yield return new WaitForSeconds (attack_speed);
			can_attack = true;
		}
	}

	private IEnumerator Spawn_Hit_Indicator (GameObject enemy_to_hit){
		GameObject _hit_indicator = Instantiate (hit_Indicator, new Vector3(enemy_to_hit.transform.position.x, 3f,
			enemy_to_hit.transform.position.z), 
			Quaternion.Euler(90f,gameObject.transform.localRotation.eulerAngles.y - hit_rotate_adjust_y,gameObject.transform.localRotation.eulerAngles.z));
		NetworkServer.Spawn (_hit_indicator);
		yield return new WaitForSeconds (indicator_time);
		NetworkServer.Destroy(_hit_indicator);
		//Quaternion.Euler(
	}
}
