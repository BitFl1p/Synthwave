using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector2 input;
    public float drag;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        
    }
    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0,Camera.main.transform.eulerAngles.y,0);
        Vector3 mv = (input.x * transform.right) + (input.y * transform.forward);
        rb.velocity += new Vector3(mv.x, 0, mv.z);
        float y = rb.velocity.y;
        rb.velocity *= 1 - drag / 100;
        rb.velocity = new Vector3(rb.velocity.x, y, rb.velocity.z);
    }
    
}

