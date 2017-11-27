using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class GlobalVariable
{
	/* 0: 不通
	 * 1: 草地
	 * 2: 沙地
	 * 3: 树
	 * 4: 石头
	 * 5: 起点
	 * 6: 终点
	 * 99: path
	 */
	public static int level;

	public static float originX;

	public static float originY;

	public static int[,] map = new int[27,18] ; 

}

