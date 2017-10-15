using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using System;
=======
>>>>>>> serverJoy

public class BulletController : MonoBehaviour {

	public Rigidbody2D supply; 
	public int damage = 10;
<<<<<<< HEAD
    public GameObject explosion;
    public float radius;



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
            Collider2D[] colliders= Physics2D.OverlapCircleAll(gameObject.transform.position,radius);
            //如果炸弹碰到的是砖块，则销毁砖块  
            foreach(Collider2D collider in colliders){  
                string co_tag=collider.gameObject.tag;  
                if(co_tag == "BreakableBox" || co_tag == "Monster"){  
                    Destroy(collider.gameObject);  
                }  
            } 
            
            
        }, 2.0f));
=======
	
	void Start () 
    {
        Destroy(gameObject, 5);
>>>>>>> serverJoy
    }


    void OnCollisionEnter2D(Collision2D coll) 
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (coll.gameObject.CompareTag("BreakableBox"))
        {
        	Rigidbody2D supplyInstance = Instantiate(supply, coll.transform.position, new Quaternion(0, 0, 0, 0)) as Rigidbody2D;
            Destroy(coll.gameObject);
            Destroy(gameObject);
        } else if (coll.gameObject.CompareTag("Monster")) {
            //Destroy(coll.gameObject);
			MonsterController monster = coll.gameObject.GetComponent<MonsterController>();
			monster.takeDamage (damage);
			Debug.Log ("Enemy got hit!");
            Destroy(gameObject);

        }
    }
}
