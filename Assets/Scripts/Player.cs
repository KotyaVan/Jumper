using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 10f;
    [SerializeField] public float accelerometerMultiplier = 2.2f;

    private Rigidbody2D _rb;
    public float MaxHeight { get; private set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MovementProcess();
        FlipProcess();
    }

    private void MovementProcess()
    {
        Vector2 velocity = _rb.velocity;
        velocity.x = Acceleration;
        _rb.velocity = velocity;

        if (transform.position.y > MaxHeight)
            MaxHeight = transform.position.y;
    }

    private float Acceleration => AccelerationCounting();

    private float AccelerationCounting()
    {
        float movement = 0f;

        float keyBoardData = Input.GetAxis("Horizontal");
        float accelerometerData = Input.acceleration.x;

#if UNITY_EDITOR || UNITY_WII
        movement = keyBoardData * movementSpeed;
#else
            movement = accelerometerData * movementSpeed * accelerometerMultiplier;
#endif

        return movement;
    }

    private void FlipProcess()
    {
        var main = Camera.main;
        var screenHalfSize = main.orthographicSize * main.aspect;
        var position = transform.position;

        if (transform.position.x > screenHalfSize || transform.position.x < -screenHalfSize)
        {
            position.x = transform.position.x > 0 ? -screenHalfSize : screenHalfSize;
            transform.position = position;
        }
    }

    public void Restart()
    {
        transform.position = Vector3.zero;
        // _rb.velocity = Vector2.zero;
        MaxHeight = transform.position.x;
    }
}