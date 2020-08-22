using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    private EnemiesManager _enemiesManager;

    public void AddEnemiesManager(EnemiesManager manager)
    {
        _enemiesManager = manager;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _enemiesManager.PlayerKilled();
    }
}
