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
	private bool currentPositionIsAvaliable = false;
	private Vector3 lastMousePosition = Vector3.zero;  
	private List<GameObject> path_list = new List<GameObject>();

	private GameObject holder; //占位符

	private Vector3 WorldStartPos = Vector3.zero;

	private int x = 0;
	private int y = 0;
	public void ButtonPutDown(){
		isTouchDown = true;

	}
	// Use this for initialization
	void Start () {
		WorldStartPos = Camera.main.ScreenToWorldPoint (new Vector3(0,0));
	}



	// Update is called once per frame
	void Update () { 
//		if ((Input.GetTouch (0).phase == TouchPhase.Ended) || (Input.GetMouseButtonDown (0))) {
//			
//			}
		if(Input.GetMouseButtonUp(0) && isTouchDown){
			if (currentPositionIsAvaliable) {
				/*
					 *如果当前位置有效 则生成路径 
					 */
				GameObject path = Instantiate (pathPart);
				path.GetComponent<SpriteRenderer> ().sortingOrder = 20;
				path.transform.position = holder.transform.position;
				path_list.Add (path);
				isNew = true;
				isTouchDown = false;
				Destroy (holder);
				GlobalVariable.map [x, y] = 99;
			} else {
				/*
				 *当前位置无效 
				 */
				isNew = true;
				isTouchDown = false;
				Destroy (holder);
			}
			}
			if (isTouchDown) {
				//Vector3 offset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - lastMousePosition;  
				if (isNew) {
					//Vector2 position = Input.GetTouch (0).position;
					Vector2 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					//增加偏移量 否则零件显示位置会不精确
					position.x -= 2;
					position.y -= 2;
					holder = Instantiate (pathHolder); //create holder
					holder.transform.position = position;
					holder.GetComponent<SpriteRenderer> ().sortingOrder = 20;
					Color color = holder.GetComponent<SpriteRenderer> ().color;
					color.a = 0.7f;
					holder.GetComponent<SpriteRenderer> ().color = color;
					isNew = false;
				} else {					
					//path_list [path_list.Count - 1].transform.position += offset;
					/*
					 * 1. 获取零件信息
					 * 2. 获取当前位置的地图素材
					 * 3. 通过判断是否可以建造进行上色 绿色：可行 红色：不可行
					 */ 
					string o_name = getObjectName (Input.mousePosition.x, Input.mousePosition.y);
					GameObject root = GameObject.Find (o_name);
					holder.transform.position = root.transform.position;
					if (positionIsAvaliable (Input.mousePosition.x, Input.mousePosition.y)) {
						//change alpha
						Color color = holder.GetComponent<SpriteRenderer> ().color;
						color.r = 0f;
						color.g = 255f;
						color.b = 0f;
						holder.GetComponent<SpriteRenderer> ().color = color;
						currentPositionIsAvaliable = true;
					} else {
						//change alpha
						Color color = holder.GetComponent<SpriteRenderer> ().color;
						color.r = 255f;
						color.g = 0f;
						color.b = 0f;
						holder.GetComponent<SpriteRenderer> ().color = color;
						currentPositionIsAvaliable = false;
					}
				}

			}
			lastMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
	}

	private string getObjectName(float x,float y)
	{
		float s_x = (Camera.main.ScreenToWorldPoint(new Vector3(x,y)).x - WorldStartPos.x) / 4;
		float s_y = (Camera.main.ScreenToWorldPoint(new Vector3(x,y)).y - WorldStartPos.y) / 4;
		x = Convert.ToInt32(Math.Floor (s_x));
		y = Convert.ToInt32(Math.Floor (s_y));
		string X = x.ToString();
		string Y = y.ToString();
		return X + Y;
	}

	private bool positionIsAvaliable(float f_x, float f_y)
	{
		float s_x = (Camera.main.ScreenToWorldPoint(new Vector3(f_x,f_y)).x - WorldStartPos.x) / 4;
		float s_y = (Camera.main.ScreenToWorldPoint(new Vector3(f_x,f_y)).y - WorldStartPos.y) / 4;
		int x = Convert.ToInt32(Math.Floor (s_x));
		int y = Convert.ToInt32(Math.Floor (s_y));
		if (GlobalVariable.map [x, y] != 3) {
			return true;
		}
		return false;
	}
}
