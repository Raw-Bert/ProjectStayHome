using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class IsometricPlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1.0f;
    Rigidbody rbody;
    Animator anim;
    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPos = rbody.gameObject.transform.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputVector = Camera.main.transform.forward * verticalInput + Camera.main.transform.right * horizontalInput;
        inputVector = inputVector.normalized;
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(inputVector.x, 0, inputVector.z));
            anim.SetFloat("IsWalking", 1.0f);
        }
        else
        {
            anim.SetFloat("IsWalking", 0f);
        }
        Vector3 movement = inputVector * movementSpeed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.gameObject.transform.position = newPos;
    }
}