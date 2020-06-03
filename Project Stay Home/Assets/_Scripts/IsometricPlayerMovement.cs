using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Vector3 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputVector = new Vector3(-verticalInput, 0, horizontalInput);
        //inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector3 movement = inputVector * movementSpeed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
        
    }
}
