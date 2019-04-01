using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public int pickUps = 0;
    public int maxPickup = 5;
    private int startHealth = 100;
    public int currentHealth;
    private bool isDamaged;
    private bool isDead;

    void Start () {
        currentHealth = startHealth;
    }

    private void TakeDamage(int amount)
    {
        isDamaged = true;
        currentHealth -= amount;
        //healthSlider.value = currentHealth;
        //playerAudio.Play ();

        if (currentHealth <= 0 && !isDead)
        {
            //Death();
        }
    }

    private void Death()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //getting pickups
        if (collision.gameObject.tag == "Pickup")
        {
            if (pickUps < maxPickup)
            {
                pickUps++;
                Destroy(collision.gameObject);
            }
        }

        //hit by turret bullet
        if (collision.gameObject.tag == "TurretBullet")
        {
            TakeDamage(3);
        }
    }

    void Update () {
		
	}
}
