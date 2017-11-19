using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathPartController : MonoBehaviour {

	[SerializeField]
	private GameObject pathPart;
	[SerializeField]
	private GameObject pathHolder;

	private bool isTouchDown = false; //是否点击选中零件了按钮
	private bool isNew = true;	//要生成一个新零件吗
	private Vector3 lastMousePosition = Vector3.zero;  
	private bool isMovementTouched = false;
	private bool[] positionCheck;
	private ArrayList mapOccupiedList = new ArrayList ();

	private GameObject holder;
	private GameObject holder_parent;//组成集合

	private Vector3 WorldStartPos = Vector3.zero;

	private int touch_indicator = 0;

	public void moveIsHappening()
	{
		isMovementTouched = true;	
	}
	public void moveNotHappening()
	{
		touch_indicator = 0;
		isMovementTouched = false;
	}

	public void ButtonPutDown(){
		isTouchDown = true;
		if (Input.touchCount == 1) {
			touch_indicator = 0;
		} else {
			if (isMovementTouched)
				touch_indicator = 1;
			else
				touch_indicator = 0;
		}

	}
	// Use this for initialization
	void Start () {
		WorldStartPos = Camera.main.ScreenToWorldPoint (new Vector3(0,0));
	}



	// Update is called once per frame
	void FixedUpdate () { 
		if ((Input.GetTouch(touch_indicator).phase == TouchPhase.Ended  || (Input.touchCount == 0 )) && isTouchDown && !isMovementTouched) {
			if (currentPositionsAreAvaliable ()) {
				/*
			 	*如果当前位置有效 则生成路径 
			 	*/	
				foreach (Transform child in holder_parent.transform) {
					GameObject path = Instantiate (pathPart);
					path.transform.position = child.transform.position;
					path.GetComponent<SpriteRenderer> ().sortingOrder = 19;
				}
				foreach (string[] o in mapOccupiedList) {
					GlobalVariable.map [Int32.Parse (o [0]), Int32.Parse (o [1])] = 99;
				}

				isNew = true;
				isTouchDown = false;
				Destroy (holder_parent);
			} else {
				isNew = true;
				isTouchDown = false;
				Destroy (holder_parent);
			}
		}
		if (isTouchDown) {
			//Vector3 offset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - lastMousePosition;  
			if (isNew) {
				holder_parent = generateHolderCollections(0);
				isNew = false;
			} else {					
				/*
				 * 1. 获取零件信息
				 * 2. 获取当前位置的地图素材
				 * 3. 通过判断是否可以建造进行上色 绿色：可行 红色：不可行
				 */  
				processTheHolder(new Vector2(Input.GetTouch(touch_indicator).position.x, Input.GetTouch(touch_indicator).position.y));
			}

		}
		lastMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
	}

	private void processTheHolder(Vector2 input)
	{
		mapOccupiedList.Clear ();
		string[] o_name = getObjectName (input.x,input.y);
		//GameObject go = GameObject.Find (o_name[0]+','+o_name[1]);
		int[] x = getX(GlobalVariable.part_model_T);
		int[] y = getY (GlobalVariable.part_model_T);
		int count = 0;
		foreach(Transform child in holder_parent.transform)
		{
			string[] name = new string[1];
			name = generateName (o_name [0], o_name [1], y [count] - y [0], x [count] - x [0]);
			GameObject temp = GameObject.Find (name [0] + ',' + name [1]);
			if (temp) {
				child.transform.position = temp.transform.position;
				mapOccupiedList.Add (name);

				if (positionIsAvaliable (name [0], name [1])) {
					Color color = child.GetComponent<SpriteRenderer> ().color;
					color.r = 0f;
					color.g = 255f;
					color.b = 0f;
					child.GetComponent<SpriteRenderer> ().color = color;
					positionCheck [count] = true;
				} else {
					Color color = child.GetComponent<SpriteRenderer> ().color;
					color.r = 255f;
					color.g = 0f;
					color.b = 0f;
					child.GetComponent<SpriteRenderer> ().color = color;
					positionCheck [count] = false;
				}
			}

			count++;
		}
	}

	private bool currentPositionsAreAvaliable(){
		for (int i = 0; i < positionCheck.Length; i++) {
			if (!positionCheck [i])
				return false;
		}
		return true;
	}

	private string[] generateName(string origin_x,string origin_y, int x, int y)
	{
		return new string[]{(Int32.Parse (origin_x) + x).ToString() , (Int32.Parse (origin_y) - y).ToString()};
	}

	private GameObject generateHolderCollections(int type)
	{


 		GameObject parent = new GameObject ();
		int holder_number = 4;
		int[] x = getX(GlobalVariable.part_model_T);
		int[] y = getY (GlobalVariable.part_model_T);
		positionCheck = new bool[x.Length];
		float width = pathHolder.GetComponent<SpriteRenderer> ().bounds.size.x;
		float height = pathHolder.GetComponent<SpriteRenderer> ().bounds.size.y;
		Vector2 position = Camera.main.ScreenToWorldPoint (Input.GetTouch(touch_indicator).position);
		for (int i = 0; i < holder_number; i++) {
			GameObject holder = Instantiate (pathHolder);
			holder.GetComponent<SpriteRenderer> ().sortingOrder = 20;
			if (i == 0) {
				holder.transform.position = position;
			} else {
				Vector2 new_position = new Vector2 (position.x + (y [i] - y [0]) * width, position.y - (x [i]  -x [0]) *height );
				holder.transform.position = new_position;
			}
			holder.transform.parent = parent.transform;
		}
		return parent;

	}

	private int[] getX(int[,] model)
	{
		ArrayList array = new ArrayList ();
		for (int i = 0; i < model.GetLength(0); i++) {
			for (int j = 0; j < model.GetLength(0); j++) {
				if (model [i, j] == 1) {
					array.Add (i);
				}
			}
		}
		return array.ToArray (typeof(int) )as int[];
	}

	private int[] getY(int[,] model)
	{
		ArrayList array = new ArrayList ();
		for (int i = 0; i < model.GetLength(0); i++) {
			for (int j = 0; j < model.GetLength(0); j++) {
				if (model [i, j] == 1) {
					array.Add (j);
				}
			}
		}
		return array.ToArray (typeof(int)) as int[];
	}

	private string[] getObjectName(float x,float y)
	{
		float s_x = (Camera.main.ScreenToWorldPoint(new Vector3(x,y)).x - WorldStartPos.x) / 4;
		float s_y = (Camera.main.ScreenToWorldPoint(new Vector3(x,y)).y - WorldStartPos.y) / 4;
		x = Convert.ToInt32(Math.Floor (s_x));
		y = Convert.ToInt32(Math.Floor (s_y));
		string X = x.ToString();
		string Y = y.ToString();
		return new string[]{X,Y};
	}

	private bool positionIsAvaliable(float f_x, float f_y)
	{
		float s_x = (Camera.main.ScreenToWorldPoint(new Vector3(f_x,f_y)).x - WorldStartPos.x) / 4;
		float s_y = (Camera.main.ScreenToWorldPoint(new Vector3(f_x,f_y)).y - WorldStartPos.y) / 4;
		int x = Convert.ToInt32(Math.Floor (s_x));
		int y = Convert.ToInt32(Math.Floor (s_y));

		if (x < 0 || y < 0 || x > 127 || y > 15) 
			return false;

		if (GlobalVariable.map [x, y] != 3) {
			return true;
		}
		return false;
	}

	private bool positionIsAvaliable(string x, string y)
	{
		int X = Int32.Parse (x);
		int Y = Int32.Parse (y);

		if (X < 0 || Y < 0 || X > 127 || Y > 15) 
			return false;
		
		if (GlobalVariable.map [X,Y] != 3 && GlobalVariable.map[X,Y] != 99 && GlobalVariable.map[X,Y] != 2) {
			return true;
		}
		return false;
	}


}
