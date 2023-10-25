using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public CharacterController charControl;

    private float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;
    private bool czyPrzegrana = true;

    public Transform checkGround;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject stereoids;

    Vector3 velocity;
    bool isGrounded;
    void Update()
    {

        if (stereoids.activeSelf == true)
        {
            speed = 17f;
        }
        isGrounded = Physics.CheckSphere(checkGround.position, groundDistance, groundMask);


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        charControl.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        charControl.Move(velocity * Time.deltaTime);

    }
}
