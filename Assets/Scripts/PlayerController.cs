using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;             //Floating point variable to store the player's movement speed.
    public float bulletSpeed = 2f; 
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public Rigidbody2D Bullet;                // 子弹 Prefab

    public VJHandler jsMovement;
    private Vector3 direction;
	private PlayerHealth health;

    void OnEnable(){
		EasyButton.On_ButtonUp += On_ButtonUp;
	}
	void On_ButtonUp(string buttonName){

		if(buttonName.Equals("put_down")){
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");
			// 初始化子弹
		Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;
			// velocity直接给物体一个固定的移动速度
		bulletInstance.velocity = new Vector2(moveHorizontal * bulletSpeed, moveVertical * bulletSpeed);
		}
	}
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
		health = GetComponent<PlayerHealth> ();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        Vector2 movement;

        direction = jsMovement.InputDirection; 

        if (direction != Vector3.zero) 
        {
            movement = new Vector2 (direction.x, direction.y);
        } else {
            //Store the current horizontal input in the float moveHorizontal.
            float moveHorizontal = Input.GetAxis ("Horizontal");

            //Store the current vertical input in the float moveVertical.
            float moveVertical = Input.GetAxis ("Vertical");

            //Use the two store floats to create a new Vector2 variable movement.
            movement = new Vector2 (moveHorizontal, moveVertical);
        }

        

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce (movement * speed);

    }

    void Update() 
    {
        // float moveHorizontal = Input.GetAxis ("Horizontal");

        // //Store the current vertical input in the float moveVertical.
        // float moveVertical = Input.GetAxis ("Vertical");
        
        // if(Input.GetButtonDown("Fire1") || (Input.GetKeyDown(KeyCode.Z)) == true)
        // {
        //     // 初始化子弹
        //     Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;
        //     // velocity直接给物体一个固定的移动速度
        //     bulletInstance.velocity = new Vector2(moveHorizontal * bulletSpeed, moveVertical * bulletSpeed);
        // }
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
