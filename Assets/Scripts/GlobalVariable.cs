using System;

public static class GlobalVariable
{
	/* 0: 不通
	 * 1: 草地
	 * 2: 沙地
	 * 3: 树
	 * 99: path
	 */
	public static int[,] map = new int[30,30] ; 

	/*
	 * T 字: 0
	 */
	public static int[,] part_model_T = new int[,]{{0,0,0},{0,1,0},{1,1,1}};
}

