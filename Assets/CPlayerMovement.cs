using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Transform orientation;

    public float altezza;
    public float attrito;
    public LayerMask pavimento;
    bool aTerra;

    public float jumpForce;
    public float jumpCooldown;
    public float airMul;
    private bool readyToJump = true;

    private KeyCode JumpKey = KeyCode.Space;

    float InputOri;
    float InputVer;

    Vector3 Direzione;

    Rigidbody rb;

    [SerializeField] private GameObject PlayerObj;
    [SerializeField] private GameObject SpawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        transform.position = SpawnPoint.transform.position;

    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void MyInput()
    {
        InputOri = Input.GetAxisRaw("Horizontal");
        InputVer = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(JumpKey) && readyToJump && aTerra)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void SpeedControl()
    {
        Vector3 limitVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (limitVel.magnitude > movementSpeed)
        {
            Vector3 newVel = limitVel.normalized * movementSpeed;
            rb.linearVelocity = new Vector3(newVel.x, rb.linearVelocity.y, newVel.z);
        }
    }

    void Update()
    {
        aTerra = Physics.Raycast(transform.position, Vector3.down, altezza * 0.7f, pavimento);

        MyInput();
        SpeedControl();

        if (aTerra)
        {
            rb.linearDamping = attrito;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    private void MovePlayer()
    {
        Direzione = orientation.forward * InputVer + orientation.right * InputOri;

        if (aTerra)
        {
            rb.AddForce(Direzione.normalized * movementSpeed * 10f, ForceMode.Force);
        }
        else if (!aTerra)
        {
            rb.AddForce(Direzione.normalized * movementSpeed * 10f * airMul, ForceMode.Force);
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
}