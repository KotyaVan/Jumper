using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    private int _minHeight = 200;
    private float _chance = 0.1f;
    public bool KilledByEnemy { get; private set; }
    
    public void PlayerKilled()
    {
        KilledByEnemy = true;
    }

    public void Restart()
    {
        KilledByEnemy = false;
    }

    public bool CanCreateEnemy(int currentHeight)
    {
        if (currentHeight > _minHeight)
        {
            float random = Random.Range(0f, 1f);
            return random <= _chance;
        }

        return false;
    }
}