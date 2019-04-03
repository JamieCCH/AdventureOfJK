using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

    public Button restartBt;

    void Start()
    {
        AudioSource buttonSound = GetComponent<AudioSource>();

        restartBt.onClick.AddListener(buttonSound.Play);
        restartBt.onClick.AddListener(() => SceneManager.LoadScene("Game"));
    }


    void Exit()
    {
        Debug.Log("Quit Game!!");
        Application.Quit();
    }

}
