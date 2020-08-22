using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public bool KilledByEnemy { get; private set; }

    public void PlayerKilled()
    {
        KilledByEnemy = true;
    }

    public void Restart()
    {
        KilledByEnemy = false;
    }
}