using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Base_Unit_AI : MonoBehaviour {

	NavMeshAgent agent;
	public GameObject[] enemies;
	public string enemy_tag;
	public float attack_range;
	public bool in_range;
	GameObject to_follow;
	public float re_assign_time = 1f;
	public float attacks_per_sec = 2f;
	public bool is_attacking;
	public float distance_from;
	public bool can_attack;
	public string No_Tower;
	public string tower;
	public string enemy_unit;
	private float attack_strength;

	private IEnumerator attack_coroutine;
	private IEnumerator new_target_coroutine;

	// Use this for initialization
	void Start () {
		attack_strength = gameObject.GetComponent<SpawnableGuy> ().attack_damage;
		agent = GetComponent<NavMeshAgent>();

		to_follow = null;
		//check_for_new_target ();
		//is_attacking = false;

//		if (is_attacking == false) {
//			StartCoroutine (attack_unit ());
//		}
//
//		new_target_coroutine = swap_if_not_attacking ();
		InvokeRepeating ("refresh_enemy_list", 0f, 2f);
		InvokeRepeating ("swap_if_not_attacking",0f,0.25f);
		InvokeRepeating ("refresh_attack",0f,attacks_per_sec);

	}
	
	// Update is called once per frame
	void Update () {
		
		if (to_follow != null) {
			agent.destination = to_follow.transform.position;
		}



	}

	void check_for_new_target(){
		



		float max_distance = float.MaxValue;
		//to_follow = null;
		for (int i = 0; i < enemies.Length; i++) {
			float dist_to = Vector3.Distance (enemies[i].transform.position, transform.position);
			if (dist_to < max_distance) {
				to_follow = enemies [i];
				max_distance = dist_to;
			}
		}
		if (to_follow != null) {
			agent.destination = to_follow.transform.position;
		}
	}

	void refresh_enemy_list(){
		enemies = GameObject.FindGameObjectsWithTag(enemy_tag);
	}

	bool inAttackRangeOf(GameObject target){
		float dist_to = Vector3.Distance (target.transform.position, transform.position);
		distance_from = dist_to;
		if (dist_to <= attack_range) {
			in_range = true;
			return true;
		} else {
			in_range = false;
			return false;
		}
	}

	private IEnumerator attack_unit(){
		//TODO: subtract health from target
		is_attacking = true;

		if(to_follow != null && inAttackRangeOf(to_follow)){
			
			//deal damage
			Debug.Log("attacked");
		}

		Debug.Log ("Attempting To Attack");
		yield return new WaitForSeconds (attacks_per_sec);
	}

	public void swap_if_not_attacking(){
		//Debug.Log ("Swapping");
		if (to_follow != null) {
			if (!inAttackRangeOf (to_follow)) {
				check_for_new_target ();
				in_range = false;
			} else {
				in_range = true;
			}
		} else {
			check_for_new_target ();
		}
	}

	public void refresh_attack(){
		//Debug.Log ("REPEAT PLEASE");
		if (in_range && to_follow != null) {
			String _tag = to_follow.tag;

			if (_tag.Equals (No_Tower) && !(to_follow.GetComponent<NoTowerScript> ().Equals(null))) {
				NoTowerScript plot = to_follow.GetComponent<NoTowerScript> ();
				plot.currentHealth -= (int) attack_strength;
				Debug.Log ("Attacked Plot");

			} else if (_tag.Equals (tower) && !(to_follow.GetComponent<TowerScript> ().Equals(null))) {
				TowerScript tower = to_follow.GetComponent<TowerScript> ();
				tower.currenthealth -= (int) attack_strength;
				Debug.Log ("Attacked Tower");

			} else if (_tag.Equals (enemy_tag) && !(to_follow.GetComponent<SpawnableGuy> ().Equals(null))) {
				SpawnableGuy unit = to_follow.GetComponent<SpawnableGuy> ();
				unit.current_health -= attack_strength;
				Debug.Log ("Attacked Unit");
			}



//			try{
//				Debug.Log ("ATTACK");
//				SpawnableGuy unit = to_follow.GetComponent<SpawnableGuy>();
//				unit.current_health -= gameObject.GetComponent<SpawnableGuy>().attack_damage;
//			}catch(Exception e){
//				Debug.Log ("Thats A Tower");
//			}
		}
	}
}
