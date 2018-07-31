using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public int strength = 8;
	public int return_strength = 2;

	public GameObject player;
	Vector3 distance_to_player = Vector3.zero;
	float range = 5;

	//public GameObject spawner;

	Vector3 next_pos;
	Vector3 current_pos;

	NavMeshAgent agent;
	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();

		player = GameObject.Find("player");
		//spawner = GameObject.Find("enemy_spawner");

		next_pos = transform.position;
		current_pos = transform.position;
		InvokeRepeating("EnemyStuck", 3, 1);
	}
	
	void Update () {
		if (player != null) {
			distance_to_player = player.transform.position - transform.position;
			if (distance_to_player.magnitude < range && this.strength > player.GetComponent<PlayerController>().strength)
			{
				next_pos = player.transform.position;
			}
		}

		if((int)next_pos.x == (int)transform.position.x  && (int)next_pos.z == (int)transform.position.z)
		{
			Vector3 random_point  = Random.onUnitSphere * 7 + transform.position;
			//Debug.Log("Random point = " + random_point);
			next_pos = new Vector3((int)random_point.x, 1.6f, (int)random_point.z);
		}
	}

	private void FixedUpdate()
	{
		agent.SetDestination(next_pos);
	}

	void EnemyStuck()
	{
		if((int)transform.position.x == (int)current_pos.x && (int)transform.position.z == (int)current_pos.z)
		{
			Vector3 random_point = Random.onUnitSphere * 10 + transform.position;
			//Debug.Log("Random point = " + random_point);
			//temp_ball.transform.SetParent(transform);

			next_pos = new Vector3((int)random_point.x, 1.6f, (int)random_point.z);
		}
		current_pos = transform.position;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0, 1, 1);
		Gizmos.DrawWireSphere(transform.position, range);

		Gizmos.color = new Color(1, 0, 1);
		Gizmos.DrawWireSphere(transform.position, 7);

		Gizmos.color = new Color(1, 1, 0);
		Gizmos.DrawWireSphere(transform.position, 10);
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.gameObject.name == "player")
	//	{
	//		if (other.gameObject.GetComponent<PlayerController>().strength > this.strength)
	//		{
	//			spawner.GetComponent<EnemySpawner>().enemies.Remove(gameObject);
	//			Destroy(gameObject);
	//			other.gameObject.GetComponent<PlayerController>().strength += return_strength;
	//			other.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
	//		}
	//	}
	//}
}
