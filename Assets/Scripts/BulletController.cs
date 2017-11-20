using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour {

	public Rigidbody2D supply;
	public Rigidbody2D bulletBody; 
	public int damage = 10;
    public float radius;
	public float bombDelayTime;
	private Animator anim;


    public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action(); //最好IF判断一下不为空
    }

	void Start () 
    {
		anim = GetComponent<Animator> ();
		bulletBody = GetComponent<Rigidbody2D> ();
        StartCoroutine(DelayToInvokeDo(() =>
        {
            if(gameObject == null) {
                return;
            }
			Debug.Log("stop the bullet");
			bulletBody.isKinematic = true;
			bulletBody.velocity = Vector3.zero;
			anim.SetTrigger("explode");
		}, bombDelayTime));
    }

	void finishExplosion() {
		Collider2D[] colliders= Physics2D.OverlapCircleAll(gameObject.transform.position,radius);
		//如果炸弹碰到的是砖块，则销毁砖块  
		foreach(Collider2D collider in colliders){  
			string co_tag=collider.gameObject.tag;  
			if (co_tag == "BreakableBox") {  
				Destroy (collider.gameObject);  
			} else if (co_tag == "Monster") {
				MonsterController monCtrl = collider.gameObject.GetComponent<MonsterController> ();
				if (monCtrl != null) {
					monCtrl.takeDamage (damage);
				}
			}
		}
		Destroy (gameObject);
	}
}
