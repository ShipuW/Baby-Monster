using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour {

	public float speed;             //Floating point variable to store the player's movement speed.

    public float bulletSpeed = 10f; 
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public Rigidbody2D Bullet;                // 子弹 Prefab
	public float movespeed = 1f;

	public Vector3 InputDirection;
	private PlayerHealth health;
	public Vector2 bulletPosition;
	public Vector3 move_offset;

	public void On_SkillJoystickUp( ){

		//float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		//float moveVertical = Input.GetAxis ("Vertical");
			// 初始化子弹
		Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position+move_offset, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;
		// velocity直接给物体一个固定的移动速度
		Vector2 LocalPos = new Vector2(transform.position.x,transform.position.y);
		Vector2 LocalForward =bulletPosition;
		Vector2 VecSpeed = LocalForward - LocalPos;
		bulletInstance.velocity = new Vector2(VecSpeed.x,VecSpeed.y);

	}

	public void On_SkillJoystickMove(Vector2 move){
		float x = 0;
		float y = 0;
		float off_set = 1;
		if (move.x > 0) {
			x = off_set;
		}else if (move.x <0){
			x = -off_set;
		}
		if (move.y > 0) {
			y = off_set;
		} else if (move.y < 0) {
			y = -off_set;
		}
		move_offset = new Vector3 (x, y,0);
		bulletPosition = new Vector2 (transform.position.x + move.x*10, transform.position.y + move.y*10);
	}

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
		health = GetComponent<PlayerHealth> ();

    }

	public void On_Direction_JoystickMove(Vector2 move){

		float PositionX = move.x; //获取摇杆偏摇杆中心的X坐标
		float PositionY = move.y; //获取摇杆偏离Y坐标
		Vector2 direction = Vector2.zero;
		if (PositionX != 0 || PositionY != 0) {
//			transform.LookAt(new Vector3 (transform.position.x + PositionX, transform.position.y + PositionY, 0));
//			transform.Translate (Vector3.forward * Time.deltaTime * movespeed);  
//
//			Quaternion rotation = Quaternion.LookRotation
//				(transform.position - transform.position, transform.TransformDirection(Vector3.up));
//			transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			if (PositionX > 0) {
				direction += Vector2.right;
			} 
			if (PositionX < 0 ) {
				direction += Vector2.left;
			} 
			if (PositionY > 0 ) {
				direction += Vector2.up;
			}
			if (PositionY < 0 ) {
				direction += Vector2.down;
			}
			//transform.Translate (direction * movespeed * Time.deltaTime);
			rb2d.velocity = direction.normalized * movespeed;

				
		}

	}

	public void On_Direction_JoystickMoveEnd(){
		rb2d.velocity = Vector2.zero;
		rb2d.angularVelocity = 0;
	}

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{

//		Vector2 movement;
//
//		//direction = jsMovement.InputDirection; 
//		//direction = jsMovement.InputDirection;
//		if (InputDirection != Vector3.zero) 
//		{
//			movement = new Vector2 (InputDirection.x, InputDirection.y);
//		} else {
//			//Store the current horizontal input in the float moveHorizontal.
//			float moveHorizontal = Input.GetAxis ("Horizontal");
//
//			//Store the current vertical input in the float moveVertical.
//			float moveVertical = Input.GetAxis ("Vertical");
//
//			//Use the two store floats to create a new Vector2 variable movement.
//			movement = new Vector2 (moveHorizontal, moveVertical);
//		}
//
//
//
//		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
//		rb2d.AddForce (movement * speed);

	}

    void Update() 
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Supply"))
        {
			Supply supply = other.gameObject.GetComponent<Supply>();
			health.TakeSupply (supply.value);
			Debug.Log ("Get supply of value " + supply.value);
			other.gameObject.SetActive(false);

        }
    }
}
