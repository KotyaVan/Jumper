using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] public Transform target;
    private void Update()
    {
        if (transform.position.y < target.position.y)
        {
            var position = transform.position;
            position = new Vector3(position.x, target.position.y, position.z);
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.position = position;
        }
    }

    public void Restart()
    {
        transform.position = Vector3.back;
    }
}