using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class MonsterController : MonoBehaviour {

	public Transform player;
	public int scoreValue = 10;
	public int health = 10;
	public float chaseRange = 30.0f;
	public float attackRange = 6.0f;

	PlayerHealth playerHealth;
	PathHealth pathHealth;
	private Animator anim;
	private float distToPlayer;
	private float distToPath;
	private float targetDist;
	private GameObject nearestPath;
	public float refreshPathTimer;
	private float lastRefresh = 0;
	bool targetPlayer;
	private Vector3 direction;
	[SerializeField]
	GameObject indicator;
	EnemyIndicatorManager indicatorManager;
	int attackDamage = 10;
	int MoveSpeed = 4;
	Camera camera;
	bool isDead;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		anim = GetComponent<Animator> ();
		anim.SetInteger ("StateNum", 0);
		targetPlayer = true;
		camera = Camera.main;
		indicatorManager = GameObject.FindGameObjectWithTag ("EnemyIndicatorManager").GetComponent<EnemyIndicatorManager>();
		indicator =indicatorManager.addIndicator ();
		isDead = false;
	}

	// Update is called once per frame
	void Update () {
		if (isDead) {
			return;
		}
		GetNearestPathBlock ();
		distToPlayer = Vector2.Distance (transform.position, player.position);
		distToPath = nearestPath == null? float.MaxValue : Vector2.Distance (transform.position, nearestPath.transform.position);
		if (distToPlayer < distToPath) {
			targetPlayer = true;
			direction = Vector3.Normalize (player.position - transform.position);
			anim.SetFloat ("MoveX", direction.x);
			anim.SetFloat ("MoveY", direction.y);
			if (distToPlayer > chaseRange || playerHealth.currentHealth <= 0) {
				idle ();
			} else if (distToPlayer > attackRange) {
				chase ();
			} else {
				startAttack ();
			}
		} else {
			targetPlayer = false;
			pathHealth = nearestPath.GetComponent<PathHealth> ();
			direction = Vector3.Normalize (nearestPath.transform.position - transform.position);
			anim.SetFloat ("MoveX", direction.x);
			anim.SetFloat ("MoveY", direction.y);
			if (distToPath > attackRange) {
				chase ();
			} else {
				startAttack ();
			}
		}
	}

	public void takeDamage(int value) {
		health -= value;
		if (health <= 0) {
			isDead = true;
			//anim.SetInteger ("StateNum", 3);
			anim.enabled=false;
			GetComponent<SpriteRenderer> ().color = Color.grey;
			Destroy (gameObject, 1f);
			ScoreManager.score += scoreValue;
		}
	}
	void idle() {
		anim.SetInteger ("StateNum", 0);
		indicatorManager.hideIndicator (indicator);
	}

	void chase() {
		anim.SetInteger ("StateNum", 1);
		transform.position += direction * MoveSpeed * Time.deltaTime;
		indicatorManager.hideIndicator (indicator);
		//transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);


	}
	void startAttack() {
		anim.SetFloat ("MoveX", direction.x);
		anim.SetFloat ("MoveY", direction.y);
		anim.SetInteger ("StateNum", 2);

	}
	void attack() {
		if (targetPlayer && playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage (attackDamage);
		} 
		else if (!targetPlayer && pathHealth.currentHealth > 0) {
			pathHealth.TakeDamage (attackDamage);
			Vector2 onScreenPos = GetScreenPos ();
			if (onScreenPos != Vector2.zero) {
				Debug.Log ("should display");
				indicatorManager.showIndicator (indicator, onScreenPos);
			} 
			else {
				Debug.Log ("already on screen");
				indicatorManager.hideIndicator (indicator);
			}
				
		} 
		else {
			idle ();
		}
	}


	void GetNearestPathBlock()
	{
		if (nearestPath != null && Time.time < lastRefresh + refreshPathTimer) {
			return;
		}
		GameObject[] paths = GameObject.FindGameObjectsWithTag ("PathBlock");
		if (paths.Length != 0) {
			nearestPath = paths.Aggregate ((o1, o2) => Vector3.Distance (o1.transform.position, this.transform.position) > Vector3.Distance (o2.transform.position, this.transform.position) ? o2 : o1);
		}
	}
	Vector2 GetScreenPos() {
		Vector3 screenPos = camera.WorldToViewportPoint(transform.position); //get viewport positions

		if(screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1){
			Debug.Log("already on screen, don't bother with the rest!");
			return Vector2.zero;
		}

		Vector2 onScreenPos = new Vector2(screenPos.x-0.5f, screenPos.y-0.5f)*2; //2D version, new mapping
		float max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
		onScreenPos = camera.ViewportToScreenPoint(onScreenPos/(max*2)+new Vector2(0.5f, 0.5f)); //undo mapping
		//onScreenPos = onScreenPos/(max*2)+new Vector2(0.5f, 0.5f);
		Debug.Log(onScreenPos);
		return onScreenPos;
	}
		
}
