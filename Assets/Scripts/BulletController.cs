﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour {

	[SerializeField]
	private Sprite defaultTile;

	public Rigidbody2D supply; 
	public int damage = 10;
    public GameObject explosion;
	public GameObject pickupPiece;
    public float radius;

	private Vector3 WorldStartPos
	{
		get{
			return Camera.main.ScreenToWorldPoint (new Vector3(0,0));
		}
	}



    public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action(); //最好IF判断一下不为空
    }

	void Start () 
    {

        StartCoroutine(DelayToInvokeDo(() =>
        {
            if(gameObject == null) {
                return;
            }

			GameObject explodeEffect = Instantiate(explosion,gameObject.transform.position,Quaternion.identity);  
			//获得以炸弹为中心的一定范围内的所有对象  
			// ParticleSystem hitParticles = explodeEffect.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
			// hitParticles.play();
			Destroy(gameObject);

			int x = GetXFromPosition(gameObject.transform.position.x);
			int y = GetYFromPosition(gameObject.transform.position.y);

		
			int [,] piece = new int [3, 3] {
				{0, 0, 0} ,		/*  初始化索引号为 0 的行 */
				{0, 0, 0} , 	/*  初始化索引号为 1 的行 */
				{0, 0, 0}   	/*  初始化索引号为 2 的行 */
			};
			bool explodedBox = false;
        	Collider2D[] colliders= Physics2D.OverlapCircleAll(gameObject.transform.position,radius);
        //如果炸弹碰到的是砖块，则销毁砖块  
        	foreach(Collider2D collider in colliders){  
            	string co_tag=collider.gameObject.tag;  
                if(co_tag == "Monster"){  
                    Destroy(collider.gameObject);  
                } 
				if(co_tag == "BreakableBox"){
						explodedBox = true;
						int boxX = GetXFromPosition(collider.gameObject.transform.position.x);
						int boxY = GetYFromPosition(collider.gameObject.transform.position.y);
						GlobalVariable.map[boxX,boxY] = 1;
						piece[boxX - x + 1, boxY - y + 1] = 1;
					
					Destroy(collider.gameObject);
				}
            }
			if(explodedBox) {
				GameObject pickup = Instantiate(pickupPiece,gameObject.transform.position,Quaternion.identity);
				PickupPiece pickupClass = pickup.GetComponent<PickupPiece> ();
				pickupClass.generatePieceCollections(RotatePieceAfterGenerate(piece), transform.position);
			}
			

			
            Destroy(explodeEffect,0.5f); 
            

        }, 2.0f));
    }

	int GetXFromPosition (float x){
		return (int) ((x - GlobalVariable.originX) / defaultTile.bounds.size.x);
	}
	int GetYFromPosition (float y){
		return (int) ((y - GlobalVariable.originY) / defaultTile.bounds.size.y);
	}

    void OnCollisionEnter2D(Collision2D coll) 
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (coll.gameObject.CompareTag("BreakableBox"))
        {
        	// Rigidbody2D supplyInstance = Instantiate(supply, coll.transform.position, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;
         //    Destroy(coll.gameObject);
         //    Destroy(gameObject);
        } else if (coll.gameObject.CompareTag("Monster")) {
            //Destroy(coll.gameObject);
			// MonsterController monster = coll.gameObject.GetComponent<MonsterController>();
			// monster.takeDamage (damage);
			// Debug.Log ("Enemy got hit!");
   //          Destroy(gameObject);

        }
    }

	public int[,] RotatePieceAfterGenerate(int[,] matrix)
	{
		int[,] oldMatrix = matrix;
		int[,] newMatrix = new int[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
		int newColumn = 0;
		int newRow = oldMatrix.GetLength(0) - 1;
		for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
		{
			newRow = newMatrix.GetLength(0) - 1;
			for (int oldColumn = 0; oldColumn < oldMatrix.GetLength(1); oldColumn++)
			{
				newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
				newRow--;
			}
			newColumn++;
		}
		return newMatrix;
	}
}
