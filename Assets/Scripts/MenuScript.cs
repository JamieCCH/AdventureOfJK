using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        exitBt.onClick.AddListener(Exit);
    }


    void Exit()
    {
        Debug.Log("Quit Game!!");
        Application.Quit();
    }

}
