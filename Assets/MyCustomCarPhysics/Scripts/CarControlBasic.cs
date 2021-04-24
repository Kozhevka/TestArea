using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlBasic : MonoBehaviour
{
    [Header("Wheel Collider")]

    [SerializeField] 
    private WheelCollider frontLeftCol, frontRightCol;
    [SerializeField]
    private WheelCollider backLeftCol, backRightCol;

    [Header("Wheel body renderer")]
    [SerializeField]
    private Transform frontLeftBody, frontRightBody;
    [SerializeField]
    private Transform backLeftBody, backRightBody;

    [SerializeField]
    private float steerMaxAngle = 25.0f;
    [SerializeField]
    private float engineForce = 1500f;

    [SerializeField]
    private float currentSteerAngle;

    private float horizontl, verticl;


    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        
        MoveVertical();
        SteerCar();

        UpdateWheelPos(frontLeftCol, frontLeftBody);
        UpdateWheelPos(frontRightCol, frontRightBody);
        UpdateWheelPos(backLeftCol, backLeftBody);
        UpdateWheelPos(backRightCol, backRightBody);
    }

    private void PlayerInput()
    {
        horizontl = Input.GetAxis("Horizontal");
        verticl = Input.GetAxis("Vertical");
    }
    
    private void MoveVertical()
    {
        backLeftCol.motorTorque = verticl * engineForce;
        backRightCol.motorTorque = verticl * engineForce;
    }

    private void SteerCar()
    {
        currentSteerAngle = steerMaxAngle * horizontl;
        frontLeftCol.steerAngle = currentSteerAngle;
        frontRightCol.steerAngle = currentSteerAngle;
    }

    private void UpdateWheelPos(WheelCollider col, Transform bodyTransforn)
    {
        Vector3 pos = bodyTransforn.position;
        Quaternion rot = bodyTransforn.rotation;

        col.GetWorldPose(out pos, out rot);
        bodyTransforn.position = pos;
        bodyTransforn.rotation = rot;
    }

}
