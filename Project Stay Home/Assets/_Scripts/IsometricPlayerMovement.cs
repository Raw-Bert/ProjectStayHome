using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class IsometricPlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1.0f;
    Rigidbody rbody;
    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPos = rbody.gameObject.transform.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputVector = Camera.main.transform.forward * verticalInput + Camera.main.transform.right * horizontalInput;
        inputVector = inputVector.normalized;
        Vector3 movement = inputVector * movementSpeed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.gameObject.transform.position = newPos;

        print("Veritcal output = " + verticalInput);
        print("Horizontal output = " + horizontalInput);
    }
}