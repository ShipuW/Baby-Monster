using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public int time;
	public int startTime = 60;

	Text text;


	void Awake ()
	{
		switch (GlobalVariable.level) {
		case 1:
			startTime = 60;
			break;
		case 2:
			startTime = 200;
			break;
		case 3:
			startTime = 999;
			break;
		}
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
		if (time <= 10) {
			text.text = "<color=#B71B1BFF>"+"Time:" + time+"</color>";
		}

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
