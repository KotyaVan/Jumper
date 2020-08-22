using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] public Transform target;
    void Update()
    {
        if (transform.position.y < target.position.y)
        {
            var position = transform.position;
            position = new Vector3(position.x, target.position.y, position.z);
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.position = position;
        }
    }
}