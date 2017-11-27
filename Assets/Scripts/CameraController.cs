using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

	public static GameObject cart = null; 

//	public float smoothTime = 0.01f;  //摄像机平滑移动的时间
//	private Vector3 cameraVelocity = Vector3.zero;

    // Use this for initialization
    void Start () 
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }
    
    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		if (cart == null) {
			transform.position = player.transform.position + offset;
		} else {
			transform.position = cart.transform.position + offset;
		}
//		transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + new Vector3(0, 0, -5), ref cameraVelocity, smoothTime);
    }

	public static void FollowCart(GameObject cartObject){
		cart = cartObject;
//		gameObject.transform.position = cart.transform.position;
	}
}
