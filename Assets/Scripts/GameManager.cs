using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public Camera firstPersonCamera;
    public Camera overheadCamera;
    private GameObject player;
    private bool isInFirstPersonView = false;

    public void ShowOverheadView()
    {
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;

        isInFirstPersonView = false;
    }

    void Start () {
        player = GameObject.FindWithTag("Player");
        isInFirstPersonView = player.GetComponent<PlayerController>().isFirstPerson;
        ShowOverheadView();
    }


    public void ShowFirstPersonView()
    {
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
        isInFirstPersonView = true;
    }


    void Update () {
        isInFirstPersonView = player.GetComponent<PlayerController>().isFirstPerson;
        if (isInFirstPersonView)
        {
            ShowFirstPersonView();
        }
        else
        {
            ShowOverheadView();
        }
    }
}
