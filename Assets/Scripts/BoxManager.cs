using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour {


	public Rigidbody2D roadPiece; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy () {
		Rigidbody2D roadPieceInstance = Instantiate(roadPiece, transform.position, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;
	}
}
