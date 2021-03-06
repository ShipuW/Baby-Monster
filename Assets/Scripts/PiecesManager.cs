﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : MonoBehaviour {

	public static PiecesManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

	public int [,] currentPiece = new int [3, 3] {
		{0, 0, 0} ,		/*  初始化索引号为 0 的行 */
		{0, 0, 0} , 	/*  初始化索引号为 1 的行 */
		{0, 0, 0}   	/*  初始化索引号为 2 的行 */
	};

	// Use this for initialization
	void Awake () {
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetCurrentPiece (int[,] pickup) {
		currentPiece = pickup;
	}

	public void ResetPiece() {
		currentPiece = new int [3, 3] {
			{0, 0, 0} ,		/*  初始化索引号为 0 的行 */
			{0, 0, 0} , 	/*  初始化索引号为 1 的行 */
			{0, 0, 0}   	/*  初始化索引号为 2 的行 */
		};

	}


	public void RotatePiece()
	{
		int col = currentPiece.GetLength (0);
		int row = currentPiece.GetLength(1);
		int[,] temp =new int[col,row];
		int dst = row-1;
		for (int i = 0; i < row; i++) {
			for (int j = 0; j < col; j++) {
				temp [j, dst] = currentPiece [i,j];
			}
			dst--;
		}

		for (int i = 0; i < col; i++)
			for (int j = 0; j < col; j++)
				currentPiece [i, j] = temp [i, j];
 		Debug.Log ("test");
	}
		
}
