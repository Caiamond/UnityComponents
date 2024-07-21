using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovementAcceleration : MonoBehaviour
{
    [SerializeField] private float AccelerationSpeed = 10f;
    [SerializeField] private float DecelerationSpeed = 0.2f;

    public CharacterController controller;
    public Vector2 Direction = Vector2.right;
    public Vector2 Velocity = Vector2.zero;
    public float MaxSpeed = 1.0f;

    private bool xMoved = false;
    private bool yMoved = false;

    void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    void Update()
    {

        Move();
        controller.Move(Velocity);

    }

    void Move()
    {
        Vector2 addition = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            addition.y += 1;
            yMoved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            addition.y += -1;
            yMoved = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            addition.x += 1;
            xMoved = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            addition.x += -1;
            xMoved = true;
        }

        Velocity += AccelerationSpeed * Time.deltaTime * addition.normalized;
        
        if ((xMoved && yMoved) == false)
        {
            if ((Velocity - Velocity.normalized * DecelerationSpeed * Time.deltaTime).magnitude >= 0)
            {
                Velocity -= Velocity.normalized * DecelerationSpeed * Time.deltaTime;
            }
            else
            {
                Velocity = Vector2.zero;
            }
        }
        
        print(controller.velocity.magnitude);
        print(Velocity.magnitude);

        if (Velocity.magnitude > MaxSpeed * Time.deltaTime)
        {
            Velocity = Velocity.normalized * MaxSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Velocity = Velocity.normalized * MaxSpeed * Time.deltaTime * 2.5f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Velocity = Vector2.zero;
        }

        xMoved = false;
        yMoved = false;
    }
}
