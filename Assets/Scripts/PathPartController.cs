using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathPartController : MonoBehaviour {

	[SerializeField]
	private GameObject pathPart;
	[SerializeField]
	private GameObject pathHolder;
	[SerializeField]
	private GameObject cart;
	private GameObject cartObject;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject canvas;

	private bool connected = false;

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

	private int[,] model; //碎片对应模型

	private bool rotate = false;

	private bool[,] the_path ;

	private int[] currPoint = new int[] {0, 0};
	private int[] nextPoint = new int[] {0, 0};

	Animator anim;

	public AudioClip bricksStack;

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
		the_path  = new bool[GlobalVariable.map.GetLength(0), GlobalVariable.map.GetLength(1)]; 
		anim = canvas.GetComponent<Animator>();
	}

	public void ButtonPutUp()
	{
		if (rotate) {
			rotatedCurrentModel ();
			resetPieceThumb ();
			rotate = false;
		}
	}

	private void resetPieceThumb()
	{
		GameObject thumbnail = GameObject.Find ("HoldingPiece");
		HoldingPieceManager pieceManager = thumbnail.GetComponent<HoldingPieceManager> ();
		pieceManager.updatePiece ();
	}


	// Update is called once per frame
	void FixedUpdate () { 
		try{
			if (isButtonRealsed()) {
				if (currentPositionIsOnButton ())
				{
					rotate = true;
				}
				else{
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
					PiecesManager.instance.ResetPiece();
					resetPieceThumb();
					isNew = true;
					isTouchDown = false;
					Destroy (holder_parent);
//						SoundManager.instance.RandomizeSfx (bricksStack);
					//判断是否联通
					if(existPath(GlobalVariable.map,the_path,GlobalVariable.start,GlobalVariable.end))
					{	
						
						Debug.Log("Connected!!!!!");
						if(connected == false) {
							connected = true;
							int x = GlobalVariable.start[0];
							int y = GlobalVariable.start[1];

							currPoint = GlobalVariable.start;
							nextPoint = GlobalVariable.start;
							GameObject ob = GameObject.Find(x.ToString()+','+y.ToString());
							
							cartObject = Instantiate (cart, ob.transform.position, Quaternion.identity);
							CameraController.FollowCart(cartObject);
//							player.transform.position = new Vector2(ob.transform.position.x,ob.transform.position.y);
						}
					}
				} else {
					isNew = true;
					isTouchDown = false;
					Destroy (holder_parent);
					}}
			}

			if (isTouchDown && !currentPositionIsOnButton()) {
				//Vector3 offset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - lastMousePosition;  
				if (isNew) {
					holder_parent = generateHolderCollections(2);
					isNew = false;
				} else {					
//					/*
//					 * 1. 获取零件信息
//					 * 2. 获取当前位置的地图素材
//					 * 3. 通过判断是否可以建造进行上色 绿色：可行 红色：不可行
//					 */  
					processTheHolder(new Vector2(Input.GetTouch(touch_indicator).position.x-200, Input.GetTouch(touch_indicator).position.y+200));
				}

				}
			}catch{
		}
		lastMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
	}

	void Update () {
		if(connected){
			if (compareArr (currPoint, GlobalVariable.end) == false) {
				if (compareArr (currPoint, nextPoint)) {
					int x = currPoint [0];
					int y = currPoint [1];

					the_path [x, y] = false;
					if (the_path [x - 1, y]) {
						x = x - 1;
					} else if (the_path [x + 1, y]) {
						x = x + 1;
					} else if (the_path [x, y - 1]) {
						y = y - 1;
					} else if (the_path [x, y + 1]) {
						y = y + 1;
					} else {
						anim.SetTrigger("GameWin");

					}

					nextPoint = new int[] { x, y };
				} 


				GameObject ob = GameObject.Find (nextPoint [0].ToString () + ',' + nextPoint [1].ToString ());

				int symbolX = 0;
				if (ob.transform.position.x - cartObject.transform.position.x > 1.0f) {
					symbolX = 1;
				} else if (ob.transform.position.x - cartObject.transform.position.x < -1.0f) {
					symbolX = -1;
				} else {
					symbolX = 0;
				}

				int symbolY = 0;
				if (ob.transform.position.x - cartObject.transform.position.x > 1.0f) {
					symbolX = 1;
				} else if (ob.transform.position.x - cartObject.transform.position.x < -1.0f) {
					symbolX = -1;
				} else {
					symbolX = 0;
				}
				if (symbolX != 0 || symbolY != 0) {
					cartObject.transform.position = new Vector2 ((float)(cartObject.transform.position.x + symbolX * 0.5), (float)(cartObject.transform.position.y + symbolY * 0.5));
				} else {
					currPoint = nextPoint;
					cartObject.transform.position = new Vector2 (ob.transform.position.x, ob.transform.position.y);
				}
			}

		}
	}


	public static bool compareArr(int[] arr1,int[] arr2){

		bool[] flag=new bool[arr1.Length];//初始化一个bool数组，初始值全为false；

		for(int i=0;i<arr1.Length;i++){

			for(int j=0;j<arr2.Length;j++){

				if(arr1[i]==arr2[j]) flag[i]=true;//遇到有相同的值，对应的bool数组的值设为true；

			}

		}

		foreach(var item in flag){

			if(item==false) return false; //遍历bool数组，还有false，就说明有不同的值，结果返回false。

		}

		return true;

	}

	private bool isButtonRealsed()
	{
		if (Input.GetTouch(touch_indicator).phase == TouchPhase.Ended && isTouchDown) {
			return true;
		}
		return false;
	}

	private void processTheHolder(Vector2 input)
	{
		mapOccupiedList.Clear ();
		string[] o_name = getObjectName (input.x,input.y);
		int[] x = getX(model);
		int[] y = getY (model);
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
					color.g = 0f;
					color.b = 255f;
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

		model = getCurrentModel ();
 		GameObject parent = new GameObject ();
		int holder_number = getHolderNumber(model);
		int[] x = getX(model);
		int[] y = getY(model);
		positionCheck = new bool[x.Length];
		float width = pathHolder.GetComponent<SpriteRenderer> ().bounds.size.x;
		float height = pathHolder.GetComponent<SpriteRenderer> ().bounds.size.y;
		Vector2 position = Camera.main.ScreenToWorldPoint (Input.GetTouch(touch_indicator).position+new Vector2(-200,200));
		for (int i = 0; i < holder_number; i++) {
			GameObject holder = Instantiate (pathHolder);
			holder.GetComponent<SpriteRenderer> ().sortingOrder = 20;
			Vector2 new_position = new Vector2 (position.x + (y [i] - y [0]) * width, position.y - (x [i]  -x [0]) *height );
			holder.transform.position = new_position;
			holder.transform.parent = parent.transform;
		}
		return parent;

	}

	private int[,] getCurrentModel () {
		return PiecesManager.instance.currentPiece;
	}
	
	private void rotatedCurrentModel(){
		PiecesManager.instance.RotatePiece ();
	}
		
	private int getHolderNumber(int[,] model)
	{
		int count = 0;
		for (int i = 0; i < model.GetLength (0); i++) {
			for (int j = 0; j < model.GetLength (1); j++) {
				if (model [i, j] == 1)
					count++;
			}
		}
		return count;
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
		

	private bool positionIsAvaliable(string x, string y)
	{
		int X = Int32.Parse (x);
		int Y = Int32.Parse (y);

		if (X < 0 || Y < 0 || X >= 27 || Y >= 18) 
			return false;
		
		if (GlobalVariable.map[X,Y] != 99 && GlobalVariable.map[X,Y] != 2 && GlobalVariable.map[X,Y] != 3 && GlobalVariable.map[X,Y] != 4 && GlobalVariable.map[X,Y] != 5 && GlobalVariable.map[X,Y] != 6) {
			return true;
		}
		return false;
	}

	private bool existPath(int[,] map, bool[,] path, int[] startPoint, int[] endPoint) {
		if (startPoint[0] == endPoint[0] && startPoint[1] == endPoint[1]) {
			return true;
		}
		int m = map.GetLength(0); 
		int n = map.GetLength(1);
		String[,] prev = new String[m, n];
		Queue<int[]> front = new Queue<int[]> ();
		Queue<int[]> end = new Queue<int[]> ();
		HashSet<String> endVisited = new HashSet<String> ();
		HashSet<String> startVisited = new HashSet<String> ();
		int[,] dir = new int[,]{ { 0, -1 }, { 0, 1 }, { 1, 0 }, {-1, 0}};
		front.Enqueue (startPoint);
		startVisited.Add (toString(startPoint));
		end.Enqueue (endPoint);
		endVisited.Add (toString(endPoint));
		while (front.Count != 0 && end.Count != 0) {
			int size = front.Count;
			for (int i = 0; i < size; i++) {
				int[] current = front.Dequeue ();
				for (int di = 0; di < 4; di++) {
					int[] next = new int[]{current[0] + dir [di, 0], current[1] + dir [di, 1]};
					String s = toString(next);
					if (endVisited.Contains (s)) {
						backtracking (next, prev, path);
						backtracking (current, prev, path);
						return true;
					}
					if (next[0] >= 0 && next[0] < m && next[1] >= 0 && next[1] < n && map[next[0], next[1]] == 99 && !startVisited.Contains (s)) {
						prev [next [0], next [1]] = toString (current);
						front.Enqueue (next);
						startVisited.Add (s);
					}

				}
			}
			if (front.Count > end.Count) {
				Queue<int[]> tmp = front;
				front = end;
				end = tmp;
				HashSet<String> tmpSet = startVisited;
				startVisited = endVisited;
				endVisited = tmpSet;
			}
		}
		return false;
	}
	static String toString(int[] input) {
		return input [0] + "," + input [1];
	}

	static void backtracking(int[] point, String[,] prev, bool[,] path) {
		if (prev[point[0], point[1]] == null || prev[point[0], point[1]].Length <= 0) {
			return;
		}
		path [point[0], point[1]] = true;
		backtracking (splitToAxis (prev [point [0], point [1]]), prev, path);
	}
	static int[] splitToAxis(String s) {
		String[] split = s.Split (',');
		return new int[]{ Int32.Parse (split [0]), Int32.Parse (split [1]) };
	}



	private bool currentPositionIsOnButton()
	{
		GameObject button = GameObject.Find("PutPart");
		float width = 120;
		float height = 120;

		Vector2 input = Input.GetTouch (touch_indicator).position;
		Vector3 postion = button.transform.position;
		if ((input.x < postion.x + width / 2) && (input.x > postion.x - width / 2) && (input.y < postion.y + height / 2) && (input.y > postion.y - height / 2)) {
			return true;
		} else {
			return false;
		}
	}

}
