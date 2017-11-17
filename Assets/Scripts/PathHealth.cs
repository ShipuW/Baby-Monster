using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHealth : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;

	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	PlayerController playerController;
	SpriteRenderer spriteRenderer;
	bool isDestroyed;
	bool damaged;


	void Start () {
		currentHealth = startingHealth;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(damaged)
		{
			Debug.Log ("Path is attacked!");
			spriteRenderer.color = flashColour;
		}
		else
		{
			spriteRenderer.color = Color.Lerp (spriteRenderer.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		if(currentHealth <= 0 && !isDestroyed)
		{
			DestroyPath ();
		}
	}
	void DestroyPath ()
	{
		isDestroyed = true;

		Destroy (gameObject, .5f);
		Debug.Log ("One Path is dead!");
		//playerShooting.enabled = false;
	}
}
