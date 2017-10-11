using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterController : MonoBehaviour {

	public Transform target;
	float minDist = 10.0f;

	//private BoxCollider2D boxCollider;
	//private Rigidbody2D rb2D;
	public float smoothTime = 5.0f;
	//Vector3 used to store the velocity of the enemy
	private Vector3 smoothVelocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag ("Player").transform;
		//boxCollider = GetComponent <BoxCollider2D> ();
		//rb2D = GetComponent <Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		float distance = Vector2.Distance (transform.position, target.position);
		if (distance < minDist) {
			//transform.LookAt (target);
			transform.position = Vector3.SmoothDamp(transform.position, target.position, ref smoothVelocity, smoothTime);
		}

	}
}
