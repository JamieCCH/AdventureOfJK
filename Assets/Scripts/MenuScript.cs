using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{

    public Button startBt;
    public Button introBt;
    public Button creditBt;
    public Button exitBt;
 

    void Start()
    {
        AudioSource buttonSound = GetComponent<AudioSource>();

        startBt.onClick.AddListener(buttonSound.Play);
        introBt.onClick.AddListener(buttonSound.Play);
        creditBt.onClick.AddListener(buttonSound.Play);
        exitBt.onClick.AddListener(buttonSound.Play);
       
        startBt.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        introBt.onClick.AddListener(() => SceneManager.LoadScene("Intro"));
        creditBt.onClick.AddListener(() => SceneManager.LoadScene("Credit"));
        exitBt.onClick.AddListener(exit);
    }


    void exit()
    {
        Debug.Log("Quit Game!!");
        Application.Quit();
    }

}
