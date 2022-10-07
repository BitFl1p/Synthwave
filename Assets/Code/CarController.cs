using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public List<WheelCollider> wheels;
    Vector2 input;
    public float drag;
    public float speed;
    public float steer;
    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();

    }
    private void Update()
    {
        wheels[0].motorTorque = input.y * speed;
        wheels[1].motorTorque = input.y * speed;
        wheels[0].steerAngle = input.x * steer;
        wheels[1].steerAngle = input.x * steer;
    }
}
