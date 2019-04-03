using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditUIScript : MonoBehaviour {

    public Button startBt;
    public Button backBt;

    void Start()
    {
        AudioSource buttonSound = GetComponent<AudioSource>();

        startBt.onClick.AddListener(buttonSound.Play);
        backBt.onClick.AddListener(buttonSound.Play);

        startBt.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        backBt.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
    }

}
