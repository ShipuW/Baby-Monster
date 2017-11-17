using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	PlayerController playerController;
	SpriteRenderer spriteRenderer;
	bool isDead;
	bool damaged;
	Animator anim;

	void Awake ()
	{
		playerController = GetComponent <PlayerController> ();
		//playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth;
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		if(damaged)
		{
			Debug.Log ("Player is attacked!");
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	public void TakeSupply (int amount) {
		currentHealth = Mathf.Max(startingHealth, currentHealth + amount);
		healthSlider.value = currentHealth;
	}

	void Death ()
	{
		playerController.enabled = false;
		isDead = true;
		anim.SetTrigger ("isDead");
		//playerShooting.DisableEffects ();
//
//		playerAudio.clip = deathClip;
//		playerAudio.Play ();


		spriteRenderer.color = Color.grey;
		Debug.Log ("Player is dead!");
		//playerShooting.enabled = false;
	}


//	public void RestartLevel ()
//	{
//		SceneManager.LoadScene (0);
//	}
}
