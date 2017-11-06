using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathPartController : MonoBehaviour {

	[SerializeField]
	private GameObject pathPart;

	private bool isTouchDown = false; //是否点击选中零件了按钮
	private bool isNew = true;	//要生成一个新零件吗
	private Vector3 lastMousePosition = Vector3.zero;  
	//private GameObject go;
	private List<GameObject> path_list = new List<GameObject>();
	private bool isHoldingPart = false; //是否正在拖动零件


	private Vector3 WorldStartPos = Vector3.zero;

	public void ButtonPutDown(){
		isTouchDown = true;

	}
	// Use this for initialization
	void Start () {
		WorldStartPos = Camera.main.ScreenToWorldPoint (new Vector3(0,0));
	}



	// Update is called once per frame
	void Update () { 
		//if ((Input.GetTouch (0).phase == TouchPhase.Began)||(Input.GetMouseButtonDown(0))) {
		if(Input.GetMouseButtonDown(0)){
			if (isHoldingPart) {
				GameObject go = path_list [path_list.Count - 1];
				string o_name = getObjectName (Input.mousePosition.x, Input.mousePosition.y);
				GameObject root = GameObject.Find(o_name);
				//change alpha
				Color color = go.GetComponent<SpriteRenderer> ().color;
				color.a = 1f;
				go.GetComponent<SpriteRenderer> ().color = color;
				go.transform.position = root.transform.position;
				isHoldingPart = false;
				isTouchDown = false;
				isNew = true;
			}
		}
		if (isTouchDown) {
			Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;  
			if (isNew) {
				
				//Vector2 position = Input.GetTouch (0).position;
				Vector2 position =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//增加偏移量 否则零件显示位置会不精确
				position.x -= 2;
				position.y -= 2;
				GameObject go = Instantiate (pathPart);
				go.transform.position =  position;
				go.GetComponent<SpriteRenderer> ().sortingOrder = 12;
				Color color = go.GetComponent<SpriteRenderer> ().color;
				color.a = 0.7f;
				go.GetComponent<SpriteRenderer> ().color = color;
				isNew = false;
				isHoldingPart = true;
				path_list.Add (go);
			} else {
				//go.transform.position += offset;
				path_list[path_list.Count-1].transform.position += offset;
			}

		}
		lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
	}

	private string getObjectName(float x,float y)
	{
		float s_x = (Camera.main.ScreenToWorldPoint(new Vector3(x,y)).x - WorldStartPos.x) / 4;
		float s_y = (Camera.main.ScreenToWorldPoint(new Vector3(x,y)).y - WorldStartPos.y) / 4;



		string X = Math.Floor (s_x).ToString();
		string Y = Math.Floor (s_y).ToString();
		return X + Y;
	}

//	private string positionIsAvaliable()
//	{
//	}
}
