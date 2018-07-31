using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public int number_of_enemies;
	public GameObject enemy_prefab;
	public List<GameObject> enemies;

	public GameObject player;

	void Start () {
		InvokeRepeating("Enemy_Spawn", 2, 1);
	}
	
	void Update () {
		
	}
	
	void Enemy_Spawn()
	{
		if (player != null)
		{
			if (enemies.Count < number_of_enemies)
			{
				Vector3 random_point = Random.insideUnitSphere * 20 + new Vector3(10, 0, 10);
				Vector3 spawn_point = new Vector3(random_point.x, 1.5f, random_point.z);
				//Debug.Log(spawn_point);
				GameObject new_enemy = Instantiate(enemy_prefab, spawn_point, Quaternion.identity);
				new_enemy.transform.SetParent(transform);
				enemies.Add(new_enemy);
				SetEnemyAttributes(new_enemy);
			}
		}
	}

	void SetEnemyAttributes(GameObject new_enemy)
	{
		int min_strength = player.GetComponent<PlayerController>().strength - 5;	//generate random strength value between:
		int max_strength = player.GetComponent<PlayerController>().strength + 5;	//player's strength - 5 and player's strength + 5
		int enemy_strength = Random.Range((int)min_strength, (int)max_strength);    //generate a random strength value
		Debug.Log("Enemy "+new_enemy.name+" strength=" + enemy_strength);
		new_enemy.GetComponent<EnemyController>().strength = enemy_strength;		//assign rangom strength generated to new_enemy
		new_enemy.transform.localScale = new Vector3((float)enemy_strength / 5, (float)enemy_strength / 5, (float)enemy_strength / 5);	//enemy size depends upon enemy strength
		new_enemy.GetComponent<EnemyController>().return_strength = 1;			//when enemy is eaten by player how much strength should player get is decided by return_strength
	}
}
