using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

    GameObject gameManager;

    private float moveSpeed = 5.0f;
    private float rotationSpeed = 100.0f;
    private float jumpForce = 4.5f;

    private float interpolation = 10.0f;
    private float m_currentV = 0f;
    private float m_currentH = 0f;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0f;
    private float m_minJumpInterval = 0.25f;

    private bool isGrounded;
    private bool wasGrounded;
    private Animator playerAnim;

    private Rigidbody m_rb;
    private List<Collider> m_collisions = new List<Collider>();


    public bool isFirstPerson = false;

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
        m_rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //isGrounded |= collision.gameObject.name == "Terrian";
        //Debug.Log("stay :" + isGrounded);

        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //isGrounded |= collision.gameObject.name == "Terrian";
        //Debug.Log("stay :" + isGrounded);

        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //isGrounded &= collision.gameObject.name != "Terrian";
        //Debug.Log("exit :" + isGrounded);
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        isGrounded &= m_collisions.Count != 0;
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && isGrounded && Input.GetButton("Jump"))
        {
            m_jumpTimeStamp = Time.time;
            m_rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (!wasGrounded && isGrounded)
        {
            playerAnim.SetTrigger("Land");
        }

        if (!isGrounded && wasGrounded)
        {
            playerAnim.SetTrigger("Jump");
        }
    }

    private void DirectMoveMode()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        m_currentV = Mathf.Lerp(m_currentV, z, Time.deltaTime * interpolation);
        m_currentH = Mathf.Lerp(m_currentH, x, Time.deltaTime * interpolation);

        Transform CurrentCamera;

        if (isFirstPerson)
        {
            transform.position += transform.forward * m_currentV * moveSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * rotationSpeed * Time.deltaTime, 0);

            playerAnim.SetFloat("MoveSpeed", m_currentV);
        }
        else
        {
            CurrentCamera = GameObject.Find("Main Camera").GetComponent<Camera>().transform;
            Vector3 direction = CurrentCamera.forward * m_currentV + CurrentCamera.right * m_currentH;
            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * interpolation);
                transform.rotation = Quaternion.LookRotation(m_currentDirection);
                transform.position += m_currentDirection * moveSpeed * Time.deltaTime;
                playerAnim.SetFloat("MoveSpeed", direction.magnitude);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //In a smaller area, switch to first person view
        isFirstPerson |= other.gameObject.name == "FirstPersonArea";
    }

    private void OnTriggerExit(Collider other)
    {
        isFirstPerson &= other.gameObject.name != "FirstPersonArea";
    }

    private void MovementUpdate()
    {
        DirectMoveMode();
        JumpingAndLanding();
    }

    void Update()
    {
        playerAnim.SetBool("Grounded", isGrounded); 

        MovementUpdate();

        wasGrounded = isGrounded;

    }

}
