using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : MonoBehaviour {


	public int [,] a = new int [3, 3] {
		{0, 0, 0} ,		/*  初始化索引号为 0 的行 */
		{0, 0, 0} , 	/*  初始化索引号为 1 的行 */
		{0, 0, 0}   	/*  初始化索引号为 2 的行 */
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	static int[,] RotateMatrixCounterClockwise(int[,] oldMatrix)
	{
	    int[,] newMatrix = new int[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
	    int newColumn, newRow = 0;
	    for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
	    {
	        newColumn = 0;
	        for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
	        {
	            newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
	            newColumn++;
	        }
	        newRow++;
	    }
	    return newMatrix;
	}
}
