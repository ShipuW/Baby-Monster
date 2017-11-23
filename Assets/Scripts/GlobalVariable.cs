﻿using System;

public static class GlobalVariable
{
	/* 0: 不通
	 * 1: 草地
	 * 2: 沙地
	 * 3: 树
	 * 99: path
	 */

	public static float originX;

	public static float originY;

	public static int[,] map = new int[27,18] ; 

	/*
	 * T 字: 0
	 */
	public static int[,] part_model_T = new int[,]{{0,0,0},{0,1,0},{1,1,1}};
	/*
	 * 1 字: 1 
	 */
	public static int[,] part_model_1 = new int[,]{{0,1,0},{0,1,0},{0,1,0}};
	/*
	 * L 字: 2 
	 */
	public static int[,] part_model_L = new int[,]{{1,0,0},{1,0,0},{1,1,1}};


}

