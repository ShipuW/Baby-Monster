using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LevelManager : MonoBehaviour {


	[SerializeField]
	private Texture2D[] mapData;
	[SerializeField]
	private MapElement[] mapElements;
	[SerializeField]
	private GameObject player;

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
		GlobalVariable.originX = WorldStartPos.x;
		GlobalVariable.originY = WorldStartPos.y;
		map = GameObject.Find ("MapCollection").transform;
		int start;
		switch (GlobalVariable.level) {
		case 1:
			start = 0;
			break;
		case 2:
			start = 2;
			break;
		case 3:
			start = 4;
			break;
		default:
			start = 1;
			break;
		}
		for (int i = start; i < mapData.Length && i < start + 2; i++) {
			for (int x = 0; x < mapData [i].width; x++) {
				for (int y = 0; y < mapData [i].height; y++) {
					Color c = mapData [i].GetPixel (x, y);
					MapElement newElement = Array.Find(mapElements,e => e.MyColor == c);
					if (newElement != null) {

						initMap(ColorUtility.ToHtmlStringRGBA(c),x,y);//init map

						GameObject go = Instantiate (newElement.MyElementPrefab);

						float x_position = GlobalVariable.originX + defaultTile.bounds.size.x * (x - 1) + go.GetComponent<SpriteRenderer> ().bounds.size.x;
						float y_position = GlobalVariable.originY + defaultTile.bounds.size.y * (y - 1) + go.GetComponent<SpriteRenderer> ().bounds.size.y;


						if (ColorUtility.ToHtmlStringRGBA (c) == "EA0FE7FF") {
							player = GameObject.FindGameObjectWithTag("Player");
							player.transform.position = new Vector2 ((float) (x_position + 0.5 * defaultTile.bounds.size.x), (float) (y_position + 0.5 * defaultTile.bounds.size.y));
						}

						go.transform.position = new Vector2(x_position,y_position); 
						go.name = x.ToString()+',' + y.ToString();
						if (newElement.MyTileTag == "Tree") {
							go.GetComponent<SpriteRenderer> ().sortingOrder = 11;
						}
						if (newElement.MyTileTag == "Box") {
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
		case "B3DFE1FF": //grass
			GlobalVariable.map [x, y] = 1;
			break;
		case "EA0FE7FF": //grass with player
			GlobalVariable.map [x, y] = 1;
			break;
		case "FF9F23FF": //box
			GlobalVariable.map [x, y] = 2;
			break;
		case "35C419FF": // tree
			GlobalVariable.map [x, y] = 3;
			break;
		case "323F3FFF": // stone
			GlobalVariable.map [x, y] = 4;
			break;
		case "FF0000FF": // start
			GlobalVariable.map [x, y] = 5;
			break;
		case "0000FFFF": // end
			GlobalVariable.map [x, y] = 6;
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
