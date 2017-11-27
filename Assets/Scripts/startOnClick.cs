using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startOnClick : MonoBehaviour {



	public void LoadScene(string sceneName) {
		if (gameObject.tag == "start_easy") {
			GlobalVariable.level = 1;
		}
		if (gameObject.tag == "start_medium") {
			GlobalVariable.level = 2;
		}
		if (gameObject.tag == "start_hard") {
			GlobalVariable.level = 3;
		}
			
		SceneManager.LoadScene (sceneName);
	}
}
