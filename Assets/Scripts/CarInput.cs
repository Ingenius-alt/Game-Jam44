using UnityEngine;

public class CarInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Movement car;
    void Awake()
    {
        car = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        car.SetInputVector(inputVector);
    }
}
