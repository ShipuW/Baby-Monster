using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float movespeed = 1;
	public Vector3 InputDirection;
	void OnEnable(){
		EasyJoystick.On_JoystickMove += On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
	}
		
	void On_JoystickMove(MovingJoystick move){
		
		if (move.joystickName != "joystick") {
			return;
		}

		float PositionX = move.joystickAxis.x; //获取摇杆偏摇杆中心的X坐标
		float PositionY = move.joystickAxis.y; //获取摇杆偏离Y坐标
		if (PositionX != 0 || PositionY != 0) {
			InputDirection = new Vector3 (PositionX, PositionY, 0); 
			transform.position = new Vector3 (transform.position.x + PositionX, transform.position.y + PositionY, 0);;
			transform.Translate(Vector3.forward * Time.deltaTime * movespeed);  

		}
	}

	void On_JoystickMoveEnd(MovingJoystick move){
		InputDirection = Vector3.zero;
	}

	// Use this for initialization
	void Start () {
		InputDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
