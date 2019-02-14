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

    private float bulletSpeed = 30f;
    private float lifeSpan = 2.5f;

    private bool handUp = false;
    private bool isArmed = false;
    private bool hasGun = false;
    private Animator playerAnim;

    [SerializeField] private Transform BulletSpawn;
    [SerializeField] private GameObject BulletPrefab;

    void Start () {
        lightsaber = GameObject.FindGameObjectWithTag("Lightsaber");
        gun = GameObject.FindGameObjectWithTag("Gun");
        playerAnim = GameObject.Find("Astronaut").GetComponent<Animator>();

        lightsaber.SetActive(isArmed);
        gun.SetActive(isArmed);
    }


    void LightsaberAtk()
    {
        playerAnim.SetTrigger("SaberAtk");
    }

    void GunShoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation) as GameObject;
        Rigidbody bulletTempRB = bullet.GetComponent<Rigidbody>();
        bulletTempRB.velocity = transform.forward * bulletSpeed;

        Destroy(bullet, lifeSpan);
    }

    IEnumerator Armed()
    {
        handUp = !handUp;
        playerAnim.SetBool("IsArmed", handUp);

        yield return new WaitForSeconds(0.18f);
        isArmed = playerAnim.GetBool("IsArmed");
    }

    void Update () {

        //weapon armed
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine("Armed");
        }

        if(isArmed && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.U)))
        {
            if (hasGun)
            {
                Debug.Log("shoot");
                GunShoot();
            }
            else
            {
                Debug.Log("cut");
                LightsaberAtk();
            }
        }
       

        //switch weapon
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (m_weaponSelected == WeaponSelected.Lightsaber)
            {
                m_weaponSelected = WeaponSelected.Gun;
                lightsaber.SetActive(false);
                playerAnim.SetBool("IsSaber", false);
            }
            else
            {
                m_weaponSelected = WeaponSelected.Lightsaber;
                gun.SetActive(false);
                playerAnim.SetBool("IsSaber", true);
            }
        }

        switch (m_weaponSelected)
        {
            case WeaponSelected.Lightsaber:
                lightsaber.SetActive(isArmed);
                playerAnim.SetBool("IsSaber", true);
                hasGun = false;
                break;

            case WeaponSelected.Gun:
                gun.SetActive(isArmed);
                playerAnim.SetBool("IsSaber", false);
                hasGun = true;
                break;

            default:
                Debug.LogError("No other weapon mode");
                break;
        }


    }
}
