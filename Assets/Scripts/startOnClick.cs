using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startOnClick : MonoBehaviour {

	public void LoadScene(string sceneName) {
		if (gameObject.tag == "start_easy") {

		}
		if (gameObject.tag == "start_medium") {

		}
		if (gameObject.tag == "start_hard") {

		}
		SceneManager.LoadScene (sceneName);
	}
}
