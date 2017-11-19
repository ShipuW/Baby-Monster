using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
	public PlayerHealth playerHealth;
	public GameObject monster;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	void Spawn() {
		if (playerHealth.currentHealth <= 0f) {
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (monster, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
	}
}
