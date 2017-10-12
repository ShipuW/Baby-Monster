using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterAttack : MonoBehaviour
{
	float timeBetweenAttacks = 1.5f;
	int attackDamage = 10;

	//Animator anim;
	GameObject player;
	PlayerHealth playerHealth;
	//EnemyHealth enemyHealth;
	bool playerInRange;
	float timer;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent<EnemyHealth>();
		//anim = GetComponent <Animator> ();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log("attack triggered");
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}


	void OnTriggerExit2D (Collider2D other)
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}


	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
		{
			Attack ();
		}

		if(playerHealth.currentHealth <= 0)
		{
			Debug.Log("player is killed!");
			//anim.SetTrigger ("PlayerDead");
		}
	}


	void Attack ()
	{
		timer = 0f;

		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		}
	}
}
