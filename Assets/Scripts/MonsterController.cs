using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterController : MonoBehaviour {

	public Transform player;
	public int scoreValue = 10;
	public int health = 10;
	float minDist = 10.0f;
	PlayerHealth playerHealth;

	//private BoxCollider2D boxCollider;
	//private Rigidbody2D rb2D;
	public float smoothTime = 5.0f;

	//Vector3 used to store the velocity of the enemy
	Vector3 smoothVelocity = Vector3.zero;
	float moveSpeed = 1f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		//boxCollider = GetComponent <BoxCollider2D> ();
		//rb2D = GetComponent <Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		float distance = Vector2.Distance (transform.position, player.position);
		if (distance < minDist && playerHealth.currentHealth > 0) {
			//transform.LookAt (target);
			transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
		}

	}
	public void takeDamage(int value) {
		health -= value;
		if (health <= 0) {
			GetComponent<SpriteRenderer> ().color = Color.grey;
			Destroy (gameObject, .5f);
			ScoreManager.score += scoreValue;
		}
	}

		
}
