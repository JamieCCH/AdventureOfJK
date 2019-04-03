using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurretBulletShoot : MonoBehaviour {
	
	public float force;
	public string targetTag;
	private GameObject FavTarget;
	private Vector3 direction;
	private Quaternion destRot;


	void Start()
	{
		FavTarget = GameObject.FindWithTag (targetTag);
		Destroy (gameObject,1.5f);
	}

	void Update()
	{
		direction = (FavTarget.transform.position - transform.position).normalized;
		destRot = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, force * Time.deltaTime );
		transform.Translate (Vector3.forward * force * Time.deltaTime);
	}

	void OnTriggerEnter(Collider c)
	{
        if (c.tag == targetTag || c.tag == "Stage")
        {
            Destroy(gameObject);
            Destroy(gameObject, 1.0f);
            Debug.Log("hit");
        }
	}

}
