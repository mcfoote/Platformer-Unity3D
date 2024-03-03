using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    //public Rigidbody rb;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;
    public Transform pivot;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        rb.velocity = new Vector3(Input.GetAxis("Horizontal")*movementSpeed, rb.velocity.y, Input.GetAxis("Vertical")*movementSpeed);

        if(Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        */

        //moveDirection = new Vector3(Input.GetAxis("Horizontal")*movementSpeed, moveDirection.y, Input.GetAxis("Vertical")*movementSpeed);
        float yStore = moveDirection.y;
        moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right*Input.GetAxis("Horizontal");
        moveDirection = moveDirection.normalized * movementSpeed;
        moveDirection.y = yStore;

        if(controller.isGrounded) {
            moveDirection.y = 0f;
            if(Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;
            }
        }
 

        moveDirection.y += Physics.gravity.y * gravityScale;
        controller.Move(moveDirection * Time.deltaTime);

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed*Time.deltaTime);
        }
    }
}
