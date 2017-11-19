using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyIndicatorManager : MonoBehaviour {

	public GameObject indicatorPrefab;
	public GameObject Canvas;
	private float minX, maxX, minY, maxY;

	void Start() {
		RectTransform canvasTrans = Canvas.GetComponent<RectTransform> ();
		float indicatorOffset = indicatorPrefab.GetComponent<RectTransform> ().rect.width / 2;
		minX = indicatorOffset;
		maxX = canvasTrans.rect.width - indicatorOffset;
		minY = indicatorOffset;
		maxY = canvasTrans.rect.height - indicatorOffset;
		Debug.Log (indicatorOffset + "," + minX + "," + maxX + "," + minY + "," + maxY);
	}

	public GameObject addIndicator() {
		GameObject item = Instantiate(indicatorPrefab);
		item.GetComponent<RectTransform>().SetParent(Canvas.transform);
		item.transform.position = Vector2.zero;
		return item;
	}
	public void removeIndicator(GameObject item) {
		Destroy (item);
	}
	public void showIndicator(GameObject item, Vector2 pos) {
		item.transform.position = getPosWithOffset(pos);
		Image image = item.GetComponent<Image> ();
		Color c = image.color;
		if (c.a == 1) {
			return;
		}
		c.a = 1;
		image.color = c;
	}
	public void hideIndicator(GameObject indicator) {
	    Image image = indicator.GetComponent<Image> ();
		Color c = image.color;
		if (c.a == 0) {
			return;
		}
		c.a = 0;
		image.color = c;
	}
	private Vector2 getPosWithOffset(Vector2 pos) {
		Debug.Log ("origin pos" + pos);


		pos.x = Mathf.Min(maxX, Mathf.Max (minX, pos.x));
		pos.y = Mathf.Min(maxY, Mathf.Max (minY, pos.y));
		Debug.Log ("updated pos" + pos);
		return pos;
	}
}