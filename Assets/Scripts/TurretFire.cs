using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour {

	public float rangeToPlayer;
	public Rigidbody bullet;
	public Transform BulletSpawnPoint;
	private GameObject Player;
	private bool firing = false;
	private float fireTime;
	private float coolDown = 3.5f;
	public bool isDead = false;

	AudioSource shootSound;

	void Start () {
		Player = GameObject.FindWithTag ("PlayerHead");
		shootSound = GetComponent<AudioSource> ();
	}


	void Update () {

		if(PlayerInRange())
		{
			transform.LookAt (Player.transform.position);
			RaycastHit hit;
			if (Physics.SphereCast (transform.position, 0.5f, transform.TransformDirection (Vector3.forward), out hit))
			{
				if(hit.transform.gameObject.tag == "Player")
				{
					if(firing == false)
					{
						firing = true;
						fireTime = Time.time;
						Quaternion leadRot = Quaternion.LookRotation (Player.transform.position - transform.position);
						Rigidbody bul = Instantiate (bullet, BulletSpawnPoint.position, leadRot);
						bul.transform.rotation = leadRot;
						shootSound.Play ();
					}
				}
			}
		}
		if (fireTime + coolDown <= Time.time)
			firing = false;	
	}

	bool PlayerInRange()
	{
		return (Vector3.Distance (Player.transform.position, transform.position) <= rangeToPlayer);
	}
}
