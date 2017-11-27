using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
	public PlayerHealth playerHealth;
	public GameObject monster;
	public float spawnTime = 3f;
//	private Vector2[] spawnPoints;
	private ArrayList spawnPoints = new ArrayList();
	private int currentCount;
	public int maxCountMonster;

	[SerializeField]
	private Sprite defaultTile;

	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		currentCount = 0;
		UpdateSpawnTime ();
	}
	
	void Spawn() {
		if (playerHealth.currentHealth <= 0f || currentCount >= maxCountMonster) {
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Count);
		Instantiate (monster, (Vector2)spawnPoints [spawnPointIndex], Quaternion.identity);
		currentCount++;
	}

	void UpdateSpawnTime () {
		spawnPoints.Clear ();
		for (int x = 0; x < GlobalVariable.map.GetLength(0); x++) {
			for (int y = 0; y < GlobalVariable.map.GetLength(1); y++) {
				if (GlobalVariable.map [x, y] == 1) {
					
					float x_position = (float) (GlobalVariable.originX + defaultTile.bounds.size.x * (x + 0.5));
					float y_position = (float) (GlobalVariable.originY + defaultTile.bounds.size.y * (y + 0.5));
					spawnPoints.Add (new Vector2 (x_position, y_position));
				}
			}
		}
	}

}
