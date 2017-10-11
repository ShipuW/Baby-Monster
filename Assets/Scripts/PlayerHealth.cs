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
	bool isDead;
	bool damaged;

	void Awake ()
	{
		playerController = GetComponent <PlayerController> ();
		//playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth;
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
		isDead = true;

		//playerShooting.DisableEffects ();

//		anim.SetTrigger ("Die");
//
//		playerAudio.clip = deathClip;
//		playerAudio.Play ();

		playerController.canMove = false;
		//playerShooting.enabled = false;
	}


//	public void RestartLevel ()
//	{
//		SceneManager.LoadScene (0);
//	}
}
