using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPiece : MonoBehaviour {

	public int[,] piece;
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
		float width = pickUpBlock.GetComponent<SpriteRenderer> ().bounds.size.x;
		float height = pickUpBlock.GetComponent<SpriteRenderer> ().bounds.size.y;
		float startX = (float) (position.x - 1.5 * width);
		float startY = (float) (position.y + 0.5 * height);
		pickupPosition = new Vector2(startX, startY);
		GameObject parent = gameObject;
		int holder_number = getHolderNumber(piece);
		if (holder_number == 0)
			return;
		int[] x = getX(piece);
		int[] y = getY(piece);


		for (int i = 0; i < holder_number; i++) {
			GameObject holder = Instantiate (pickUpBlock, position, new Quaternion(0, 0, 0, 0));
			holder.GetComponent<SpriteRenderer> ().sortingOrder = 20;
			Vector2 new_position = new Vector2 (pickupPosition.x + y [i] * width, pickupPosition.y - x[i] *height );
			holder.transform.position = new_position;
			holder.transform.parent = parent.transform;
		}
		parent.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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
			for (int j = 0; j < model.GetLength(1); j++) {
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
			for (int j = 0; j < model.GetLength(1); j++) {
				if (model [i, j] == 1) {
					array.Add (j);
				}
			}
		}
		return array.ToArray (typeof(int)) as int[];
	}

}
