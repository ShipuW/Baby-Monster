using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPiece : MonoBehaviour {

	int[,] piece;
	Vector2 pickupPosition;

	[SerializeField]
	private GameObject pickUpBlock;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generatePieceCollections(int[,] model, Vector2 position)
	{
		piece = model;
		pickupPosition = position;
		GameObject parent = gameObject;
		int holder_number = getHolderNumber(piece);
		if (holder_number == 0)
			return;
		int[] x = getX(piece);
		int[] y = getY(piece);
		float width = pickUpBlock.GetComponent<SpriteRenderer> ().bounds.size.x;
		float height = pickUpBlock.GetComponent<SpriteRenderer> ().bounds.size.y;

		for (int i = 0; i < holder_number; i++) {
			GameObject holder = Instantiate (pickUpBlock);
			holder.GetComponent<SpriteRenderer> ().sortingOrder = 20;
			Vector2 new_position = new Vector2 (pickupPosition.x + (y [i] - y [0]) * width, pickupPosition.y - (x [i]  -x [0]) *height );
			holder.transform.position = new_position;
			holder.transform.parent = parent.transform;
		}
		parent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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

}
