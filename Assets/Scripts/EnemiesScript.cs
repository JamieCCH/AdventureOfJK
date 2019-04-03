using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScript : MonoBehaviour {

    private Animator m_anim;

    private bool isSeek;
    private bool canAttack;

    public float moveSpeed;
    public float rotSpeed;
    public float slowDist;
    public float stopDist;
    public float rangeToPlayer;

    private GameObject FavTarget;
    private Vector3 targetVect;     //target position
    private Vector3 destVect;       //vector of self and target
    private Quaternion destRot;     //ratate toward the target
    private float speedFactor;

    PlayerData playerHealth;

    private int damageTake = 8;

    void Start () {
        m_anim = gameObject.GetComponent<Animator>();
        FavTarget = GameObject.FindWithTag("Player");
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
    }

    bool PlayerInRange()
    {
        return (Vector3.Distance(FavTarget.transform.position, transform.position) <= rangeToPlayer);
    }

    void Update () {

        targetVect = FavTarget.transform.position;

        if (canAttack){
            m_anim.SetTrigger("Attack");
        }
        else if (isSeek)
        {
            m_anim.SetTrigger("Move");
            m_anim.SetBool("Attack",false);
        }

        if (PlayerInRange())
        {
            transform.LookAt(FavTarget.transform.position);
            isSeek = true;
            canAttack = false;
        }else
        {
            isSeek = false;
            m_anim.SetBool("Move", false);
        }

        if (isSeek)
        {
            destVect = targetVect - transform.position;
            float distTo = destVect.magnitude;  //return the length of vector
            if (distTo > stopDist)
            {
                //move the enemy
                speedFactor = (isSeek ? 1.0F : Mathf.Clamp((distTo / slowDist), 0.01F, 1.0F));
                transform.Translate(Vector3.forward * (moveSpeed * speedFactor * Time.deltaTime));
                isSeek = true;
                canAttack = false;
            }
            //stop moving
            else if (distTo <= stopDist)
            {
                isSeek = false;
                canAttack = true;
                transform.Translate(Vector3.zero);
            }
        }
    }

    void TakingDamage()
    {
        playerHealth.TakeDamage(damageTake);
    }

    private void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "Player")
        {
            InvokeRepeating("TakingDamage", 1f, 2.3f);
        }
    }

    private void OnCollisionExit(Collision c)
    {
        if(c.gameObject.tag == "Player")
        {
            CancelInvoke("TakingDamage");
        }
    }
}
