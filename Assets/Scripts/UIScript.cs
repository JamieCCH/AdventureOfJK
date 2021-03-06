﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class UIScript : MonoBehaviour {

    PlayerData player;
    private GameObject[] BallIcons;
    private int pickupedShows = 0;
    private Slider healthSlider;

    public Button exitBt;
    public Button cancelBt;
    public GameObject PauseScreen;

    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
        BallIcons = GameObject.FindGameObjectsWithTag("BallIcon");
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();

        for (int i = 0; i < BallIcons.Length; i++)
        {
            BallIcons[i] = GameObject.Find("BallIcon" + i);
        }
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseScreen.SetActive(true);
            //Time.timeScale = 0; //PauseGame
        }

        if (PauseScreen.activeSelf == true)
        {
            Choice(Exit, ClosePanel);
        }

        if (player.isDead)
        {
            SceneManager.LoadScene("GameLose");
        }

        //adjusting health bar
        healthSlider.value = player.currentHealth;
    }

    private void LateUpdate()
    {
        //Pickups icon displayed
        var pickupNum = player.GetComponent<PlayerData>().pickUps;
        if (pickupedShows == pickupNum - 1)
        {
            var ballImg = BallIcons[pickupedShows].GetComponent<Image>();
            ballImg.color = new Color(0.035f, 1f, 1f, 1f);
            pickupedShows++;
        }

        if(pickupNum == 5)
        {
            SceneManager.LoadScene("GameWin");
        }
    }

    private void ClosePanel()
    {
        if (PauseScreen.activeSelf == true)
        {
            PauseScreen.SetActive(false);

            //Time.timeScale = 1;  //ContinueGame
        }
    }

    private void Exit()
    {
        SceneManager.LoadScene("Menu");
    }


    //pause menu options
    public void Choice(UnityAction exitEvent, UnityAction cancelEvent)
    {
        PauseScreen.SetActive(true);

        exitBt.onClick.RemoveAllListeners();
        exitBt.onClick.AddListener(ClosePanel);
        exitBt.onClick.AddListener(exitEvent);

        cancelBt.onClick.RemoveAllListeners();
        cancelBt.onClick.AddListener(ClosePanel);

        AudioSource buttonSound = GetComponent<AudioSource>();
        exitBt.onClick.AddListener(buttonSound.Play);
        cancelBt.onClick.AddListener(buttonSound.Play);

    }
}
