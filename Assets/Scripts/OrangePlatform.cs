using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrangePlatform : PlatformBase
{
    protected override int JumpForce => 8;

    private Vector3 _startPosition;
    private float _movementDistance = 1.6f;
    private int _movementVector;

    private void Start()
    {
        _startPosition = transform.position;
        _movementVector = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
    }

    private void Update()
    {
        if (_startPosition.x + _movementDistance <= transform.position.x ||
            _startPosition.x - _movementDistance >= transform.position.x)
        {
            _movementVector *= -1;
        }

        if (_movementVector > 0)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (_movementVector < 0)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
    }
}