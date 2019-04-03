using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    public Button restartBt;
    public Button exitBt;

    void Start()
    {
        AudioSource buttonSound = GetComponent<AudioSource>();

        restartBt.onClick.AddListener(buttonSound.Play);
        exitBt.onClick.AddListener(buttonSound.Play);

        restartBt.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        exitBt.onClick.AddListener(Exit);
    }


    void Exit()
    {
        Debug.Log("Quit Game!!");
        Application.Quit();
    }

}
