using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    PlayerData player;
    private GameObject[] BallIcons;
    private int pickupedShows = 0;
    private Slider healthSlider;

    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
        BallIcons = GameObject.FindGameObjectsWithTag("BallIcon");
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        //health bar adjusted
        healthSlider.value = player.currentHealth;

        //Pickups displayed 
        var pickupNum = player.GetComponent<PlayerData>().pickUps;
        if (pickupedShows == pickupNum - 1)
        {
            var ballImg = BallIcons[pickupedShows].GetComponent<Image>();
            ballImg.color = new Color(0.035f, 1f, 1f, 1f);
            pickupedShows++;
        }
    }
}
