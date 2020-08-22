using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBase : MonoBehaviour
{
    protected virtual int JumpForce => throw new NotImplementedException();
    public virtual int MinHeight => throw new NotImplementedException();
    public virtual int Probability => throw new NotImplementedException();

    private bool _disactive;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_disactive) return;
        
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D player = collision.collider.GetComponent<Rigidbody2D>();
            if (player)
            {
                var playerVelocity = player.velocity;
                playerVelocity.y = JumpForce;
                player.velocity = playerVelocity;

                AfterCollisionCallBack();
            }
        }
    }
    
    protected virtual void AfterCollisionCallBack()
    {
    }

    protected void MakeDisActive()
    {
        _disactive = true;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
    }

    public void InstantiateEnemy(Enemy enemy, EnemiesManager enemiesManager)
    {
        var currentEnemy = Instantiate(enemy, transform);
        currentEnemy.AddEnemiesManager(enemiesManager);
    }
}
