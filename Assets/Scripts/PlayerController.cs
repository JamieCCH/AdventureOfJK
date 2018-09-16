using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//isGroiunded need to be set

public class PlayerController : MonoBehaviour {

    private float speed = 10.0f;
    private float rotationSpeed = 100.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;

    private bool isGrounded = true;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
    void Update () {

        if (isGrounded)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);

            if (Input.GetButton("Jump"))
            {
                var y = gravity * Time.deltaTime;
                transform.Translate(0, y, z);
            }
        }
    }
}
