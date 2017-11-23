using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewCamController : MonoBehaviour {
	public GameObject player;
	public GameObject marker;
	// Update is called once per frame
	void Update () {
		marker.transform.position = player.transform.position;
	}
}
