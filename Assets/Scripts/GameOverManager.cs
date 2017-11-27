﻿using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public PlayerHealth playerHealth;
	public ScoreManager scoreManager;

	Animator anim;


	void Awake()
	{
		anim = GetComponent<Animator>();
	}


	void Update()
	{
		if (playerHealth.currentHealth <= 0 || scoreManager.time <= 0)
		{
			anim.SetTrigger("GameOver");

		}

	}
}
