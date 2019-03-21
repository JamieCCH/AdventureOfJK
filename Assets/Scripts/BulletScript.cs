using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour 
{

	[SerializeField] float bulletSpeed;
	[SerializeField] float lifeSpan;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.forward * bulletSpeed * Time.deltaTime);	
	}

	void OnCollisionEnter (Collision other) 
	{
		//if (other.gameObject.tag == "Enemy") {
		//	var turret = other.gameObject.GetComponentInChildren<TurretFire> ();
		//	if (!turret.isDead) 
		//	{
		//		Destroy (other.gameObject);
		//		Destroy (gameObject);
				
		//		turret.isDead = true;
			
		//	}


		//}else{
		//	Destroy (gameObject,1.0f);
		//}
	}

}