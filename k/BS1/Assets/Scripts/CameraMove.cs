using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    CharacterController characterController;
    float dx;
    float dy;
    float dz;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        dx = 0.0f;
        dy = 0.0f;
        dz = 0.0f;
    }

    private void Update()
    {
        dx = Input.GetAxis("Horizontal");
        dz = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(dx, 0.0f, dz);

        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }



}
