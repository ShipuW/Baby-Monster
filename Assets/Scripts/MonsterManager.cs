using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
	public PlayerHealth playerHealth;
	public GameObject monster;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;
	private int currentCount;
	public int maxCountMonster;

	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		currentCount = 0;
	}
	
	void Spawn() {
		if (playerHealth.currentHealth <= 0f || currentCount >= maxCountMonster) {
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (monster, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		currentCount++;
	}
}
