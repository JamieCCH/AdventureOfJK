using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

    private enum WeaponSelected
    {
        Lightsaber,
        Gun
    }

    private GameObject lightsaber;
    private GameObject gun;

    private WeaponSelected m_weaponSelected = WeaponSelected.Lightsaber;

    private bool isArmed = false;
    private bool hasGun = false;
    private Animator playerAnim;

    void Start () {
        lightsaber = GameObject.FindGameObjectWithTag("Lightsaber");
        gun = GameObject.FindGameObjectWithTag("Gun");
        playerAnim = GameObject.Find("Astronaut").GetComponent<Animator>();

        lightsaber.SetActive(isArmed);
        gun.SetActive(isArmed);
    }


    void LightsaberAtk()
    {
        lightsaber.SetActive(isArmed);
        hasGun = false;
    }

    void GunShoot()
    { 
        gun.SetActive(isArmed);
        hasGun = true;
    }

    void Update () {

        //weapon armed
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Y))
        {
            isArmed = !isArmed;
            playerAnim.SetBool("IsArmed", isArmed);
            Debug.Log(playerAnim.GetBool("IsArmed"));
            //playerAnim.SetTrigger("Jump");
        }

        if(isArmed && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.U)))
        {
            if (hasGun)
            {
                Debug.Log("shoot");
            }else
            {
                Debug.Log("cut");
            }
        }

        //switch weapon
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (m_weaponSelected == WeaponSelected.Lightsaber)
            {
                m_weaponSelected = WeaponSelected.Gun;
                lightsaber.SetActive(false);
            }
            else
            {
                m_weaponSelected = WeaponSelected.Lightsaber;
                gun.SetActive(false);
            }
        }

        switch (m_weaponSelected)
        {
            case WeaponSelected.Lightsaber:
                LightsaberAtk();
                break;

            case WeaponSelected.Gun:
                GunShoot();
                break;

            default:
                Debug.LogError("No other weapon mode");
                break;
        }


    }
}
