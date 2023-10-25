using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController charControl;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float playerSpeed = 2f;
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    public Transform camPos;
    public Transform fromHolder;

    public Transform thisTransform;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        thisTransform.rotation = Quaternion.Euler(0, fromHolder.rotation.y, 0);
        isGrounded = charControl.isGrounded;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // if (isGrounded && playerVelocity.y == 0)
        // {
        //     playerVelocity.y = 0;
        // }

        Vector3 move = (transform.right * x) + (transform.forward * z);
        charControl.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        playerVelocity.y += gravity * Time.deltaTime;
        charControl.Move(playerVelocity * Time.deltaTime);




    }
}
