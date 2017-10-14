using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public Rigidbody2D supply; 
	public int damage = 10;
	
	void Start () 
    {
        Destroy(gameObject, 5);
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
