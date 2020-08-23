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
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<Player>())
        {
            _enemiesManager.PlayerKilled();
        }
        else
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
