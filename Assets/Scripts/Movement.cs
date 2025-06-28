using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Car Settings")]
    public float accelerationF = 30f;
    public float steeringF = 3.5f;
    public float driftF = 0.95f;
    public float maxSpeed = 20f;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityVsUp = 0;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        killOrthogonalVelocity();
        ApplySteeringForce();
    }

    void ApplyEngineForce()
    {
        //Calculate how fast we are going "forward"
        velocityVsUp = Vector2.Dot(transform.up, rb.linearVelocity);

        //Limit how fast we can go "forward"
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //limit how fast we can reverse at 50% max speed
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        //limit how fast we can move at any direction
        if (rb.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }


        //Slow down car eventually if input released
        if (accelerationInput == 0)
        {
            rb.linearDamping = Mathf.Lerp(rb.linearDamping, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            rb.linearDamping = 0;
        }
        Vector2 engineforceFactor = transform.up * accelerationInput * accelerationF;

        rb.AddForce(engineforceFactor, ForceMode2D.Force);
    }

    void ApplySteeringForce()
    {   
        // Makes the car have a mininum speed to turn 
        float minSpeedToTurn = (rb.linearVelocity.magnitude / 8);
        minSpeedToTurn = Mathf.Clamp01(minSpeedToTurn);

        // updates the rotation angle based on input 
        rotationAngle -= steeringInput * steeringF * minSpeedToTurn;
        rb.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void killOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

        rb.linearVelocity = forwardVelocity + rightVelocity * driftF;
    }
}
