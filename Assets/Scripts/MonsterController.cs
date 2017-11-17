using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterController : MonoBehaviour {

	public Transform player;
	public int scoreValue = 10;
	public int health = 10;
	public float chaseRange = 30.0f;
	public float attackRange = 6.0f;
	PlayerHealth playerHealth;
	private Animator anim;
	private float distToPlayer;
	private Vector3 direction;
	//private BoxCollider2D boxCollider;
	//private Rigidbody2D rb2D;
	//public float attackDelay = 1.5f;
	int attackDamage = 10;
	int MoveSpeed = 4;
	//float lastAttackTime;

	//Vector3 used to store the velocity of the enemy
	Vector3 smoothVelocity = Vector3.zero;
	float moveSpeed = 1f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		anim = GetComponent<Animator> ();
		anim.SetInteger ("StateNum", 0);
		//boxCollider = GetComponent <BoxCollider2D> ();
		//rb2D = GetComponent <Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		distToPlayer = Vector2.Distance (transform.position, player.position);
		direction = Vector3.Normalize(player.position - transform.position);
		anim.SetFloat ("MoveX", direction.x);
		anim.SetFloat ("MoveY", direction.y);
		if (distToPlayer > chaseRange || playerHealth.currentHealth <= 0) {
			idle ();
		} 
		else if (distToPlayer > attackRange) {
			chase ();
		}
		else {
			startAttack ();
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
	void idle() {
		anim.SetInteger ("StateNum", 0);
	}

	void chase() {
		anim.SetInteger ("StateNum", 1);
		transform.position += direction * MoveSpeed * Time.deltaTime;

		//transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);


	}
	void startAttack() {
		anim.SetFloat ("MoveX", direction.x);
		anim.SetFloat ("MoveY", direction.y);
		anim.SetInteger ("StateNum", 2);

	}
	void attack() {
		if (playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage (attackDamage);
			//lastAttackTime = Time.time;
		} else {
			idle ();
		}
	}

		
}
