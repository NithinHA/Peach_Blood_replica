using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Attributes")]
	public int strength = 5;
	public int return_strength = 1;

	[Header("Character Movement")]
	public float speed;
	public float jump_force;
	public float gravity;

	public EnemySpawner spawner;

	CharacterController controller;
	private Vector3 move_direction = Vector3.zero;

	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	void Update () {
		if (controller.isGrounded)
		{
			move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			move_direction = transform.TransformDirection(move_direction);
			move_direction *= speed;
			if (Input.GetButton("Jump"))
			{
				move_direction.y = jump_force;
			}
		}
		move_direction.y -= gravity * Time.deltaTime;
		controller.Move(move_direction * Time.deltaTime);
	}

	//private void OnCollisionEnter(Collision collision)
	//{
	//	if(collision.collider.tag == "enemy")
	//	{
	//		if(collision.collider.GetComponent<EnemyController>().strength > this.strength)
	//		{
	//			Destroy(gameObject);
	//			Debug.Log("Game Over");
	//		}
	//	}
	//}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "enemy")
			if (other.gameObject.GetComponent<EnemyController>().strength > this.strength)
			{
				Destroy(gameObject);
				other.gameObject.GetComponent<EnemyController>().strength += return_strength;
				//Debug.Log("Enemy strength = " + other.gameObject.GetComponent<EnemyController>().strength);
				//Debug.Log("Game Over");
			}
			else
			{
				spawner.enemies.Remove(other.gameObject);				//remove that particular enemy instance from list of enemies so that new enemies can be spawned at its place
				this.strength += other.gameObject.GetComponent<EnemyController>().return_strength;	//decides how much player's strength should be increased by
				Destroy(other.gameObject);					//and destroys that enemy instance from scene
				transform.localScale = new Vector3((float)this.strength / 5, (float)this.strength / 5, (float)this.strength / 5);	//player size depends upon player strength
				//Debug.Log("Player strength = " + strength);
				//Debug.Log("You Win");
				
			}
	}
}
