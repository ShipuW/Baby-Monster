using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombCircleProcess : MonoBehaviour {
	
	public int maxBombNum = 3;
	public int currentBombNum = 3;


	[SerializeField]
	float speed;
	
	[SerializeField]
	Transform process;
	
	[SerializeField]
	Transform indicator;
	
	public int targetProcess{ get; set;}
	private float currentAmout = 0;
	
	// Use this for initialization
	void Start () {
		targetProcess = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentBombNum == maxBombNum)
			return;

		if (currentAmout == targetProcess) {
			currentBombNum = currentBombNum + 1;
			currentAmout = 0;
		}

		if (currentAmout < targetProcess) {
			Debug.Log("currentAmount:" + currentAmout.ToString());
			
			currentAmout += speed;
			if(currentAmout > targetProcess)
				currentAmout = targetProcess;
			indicator.GetComponent<Text>().text = ((int)currentBombNum).ToString();
			process.GetComponent<Image>().fillAmount = currentAmout/100.0f;
		}
		
	}
	
	
	public void SetTargetProcess(int target)
	{
		if(target >= 0 && target <= 100)
			targetProcess = target;
	}

}
