using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingPieceManager : MonoBehaviour {

	[SerializeField]
	private GameObject pickUpBlock;


	private int[,] piece = new int[3,3] {
		{0, 0, 0} ,		/*  初始化索引号为 0 的行 */
		{0, 0, 0} , 	/*  初始化索引号为 1 的行 */
		{0, 0, 0}   	/*  初始化索引号为 2 的行 */
	};


	bool needUpdate = true;
	// Use this for initialization
	void Start () {
		updatePiece ();
	}
	
	// Update is called once per frame
	void Update () {
		if (needUpdate) {
			updateUI();
		}
	}

	public void updatePiece () {
		piece = PiecesManager.instance.currentPiece;
		needUpdate = true;
	}

	void updateUI () {

		cleanChildren ();

		GameObject parent = gameObject;
		float width = 25;//pickUpBlock.GetComponent<SpriteRenderer> ().bounds.size.x;
		float height = 25;//pickUpBlock.GetComponent<SpriteRenderer> ().bounds.size.y;
		float startX = (float) (parent.transform.position.x - 1.5 * width);
		float startY = (float) (parent.transform.position.y + 0.5 * height);

		int holder_number = getHolderNumber(piece);
		if (holder_number == 0)
			return;
		int[] x = getX(piece);
		int[] y = getY(piece);


		for (int i = 0; i < holder_number; i++) {
			
			GameObject holder = Instantiate (pickUpBlock);
			holder.transform.SetParent (parent.transform, false);
			RectTransform m_RectTransform = holder.GetComponent<RectTransform>();
			Vector2 new_position = new Vector2 ((float)(m_RectTransform.anchoredPosition.x + y [i] * width - 1 * width), (float)(m_RectTransform.anchoredPosition.y - x[i] *height + 1 * height));
			m_RectTransform.anchoredPosition = new_position;

		}

		needUpdate = false;
	}

	void cleanChildren () {
		for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
		{
			// objectA is not the attached GameObject, so you can do all your checks with it.
			GameObject objectA = gameObject.transform.GetChild(i).gameObject;
			objectA.transform.SetParent(null);
			Destroy (objectA);
			// Optionally destroy the objectA if not longer needed
		} 
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
