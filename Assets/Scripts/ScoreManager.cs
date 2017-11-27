using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public static int time;
	public int startTime = 60;

	Text text;


	void Awake ()
	{
		text = GetComponent <Text> ();
		time = startTime;
		StartTiming ();
	}

	void StartTiming () {
		StartCoroutine(DoCountDown());  
	}

	void StopTiming () {
		StopAllCoroutines();   
	}

	void Update ()
	{
		text.text = "Time: " + time;
	}

	IEnumerator DoCountDown()  
	{  
		
		while (time > 0)  
		{  
			yield return new WaitForSeconds(1f);  
			time--;  
		}  
	} 
}
