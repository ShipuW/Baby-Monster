using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private Texture2D[] mapData;
	[SerializeField]
	private MapElement[] mapElements;

	[SerializeField]
	private Sprite defaultTile;

	[SerializeField]
	private Transform map;

	private Vector3 WorldStartPos
	{
		get{
			return Camera.main.ScreenToWorldPoint (new Vector3(0,0));
		}
	}

	// Use this for initialization
	void Start () {
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	private void GenerateMap()
	{
		for (int i = 0; i < mapData.Length; i++) {
			for (int x = 0; x < mapData [i].width; x++) {
				for (int y = 0; y < mapData [i].height; y++) {
					Color c = mapData [i].GetPixel (x, y);
					MapElement newElement = Array.Find(mapElements,e => e.MyColor == c);
					if (newElement != null) {

						initMap(ColorUtility.ToHtmlStringRGBA(c),x,y);//init map

						float x_position = WorldStartPos.x + defaultTile.bounds.size.x * x;
						float y_position = WorldStartPos.y + defaultTile.bounds.size.y * y;

						GameObject go = Instantiate (newElement.MyElementPrefab);
						go.transform.position = new Vector2(x_position,y_position); 
						go.name = x.ToString() + y.ToString();
						if (newElement.MyTileTag == "Tree") {
							go.GetComponent<SpriteRenderer> ().sortingOrder = 11;
						}
						if (newElement.MyTileTag == "Sand") {
							go.GetComponent<SpriteRenderer> ().sortingOrder = 10;
						}

						go.transform.parent = map;
					}
				}
			}
		}
	}

	private void initMap(string rgb,int x, int y)
	{
		switch (rgb) {
		case "DEF6C5FF": //grass
			GlobalVariable.map [x, y] = 1;
			break;
		case "323F3FFF": //box
			GlobalVariable.map [x, y] = 2;
			break;
		case "35C419FF": // tree
			GlobalVariable.map [x, y] = 3;
			break;
		default:
			break;
		}
	}
}

[Serializable]
public class MapElement
{
	[SerializeField]
	private string tileTage;

	[SerializeField]
	private Color color;

	[SerializeField]
	private GameObject elementPrefab;




	public GameObject MyElementPrefab
	{
		get{
			return elementPrefab;
		}
	}

	public Color MyColor
	{
		get{
			return color;
		}
	}

	public String MyTileTag
	{
		get{
			return tileTage;
		}
	}
}
