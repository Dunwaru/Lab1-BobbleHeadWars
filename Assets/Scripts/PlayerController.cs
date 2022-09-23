using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody head;
    [SerializeField] private float moveSpeed = 50.0f;
    private CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);
    }
    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(moveDirection == Vector3.zero)
        {

        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }
    }
}
