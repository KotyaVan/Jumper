using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;


public class PlatformsManager : MonoBehaviour
{
    [SerializeField] private List<PlatformBase> platfroms;

    [SerializeField] public GreenPlatform greenPlatform;
    [SerializeField] public RedPlatform redPlatform;
    [SerializeField] public YellowPlatform yellowPlatform;
    [SerializeField] public OrangePlatform orangePlatform;

    [SerializeField] public Player player;

    private List<PlatformBase> _activePlatforms = new List<PlatformBase>();
    private float _levelWidth => Camera.main.orthographicSize * Camera.main.aspect;
    private float _halfPlatformHeight => (greenPlatform.transform.localScale.y / 2);


    [SerializeField] public float minY = 0.8f;
    [SerializeField] public float maxY = 3f;

    public void Start()
    {
        GenerateFirstPlatform();
    }

    public void Update()
    {
        TryGeneratePlatforms();
        TryRemovePlatforms();
    }

    private void GenerateFirstPlatform()
    {
        //Base platform always under player
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = player.transform.position.x;
        spawnPosition.y = -Camera.main.orthographicSize + player.gameObject.transform.localScale.x * 2;
        _activePlatforms.Add(Instantiate(orangePlatform, spawnPosition, Quaternion.identity));
    }

    private void TryGeneratePlatforms()
    {
        var playerPosition = player.transform.position;
        var up = playerPosition.y + Camera.main.orthographicSize + _halfPlatformHeight;

        PlatformBase platformBase = GetRandomPlatformType();

        while (up > _activePlatforms[_activePlatforms.Count() - 1].transform.position.y)
        {
            var spawnPosition = new Vector3();
            spawnPosition.x = Random.Range(_levelWidth, -_levelWidth);
            spawnPosition.y = _activePlatforms[_activePlatforms.Count() - 1].transform.position.y +
                              Random.Range(minY, maxY);
            _activePlatforms.Add(Instantiate(platformBase, spawnPosition, Quaternion.identity));
        }
    }

    private void TryRemovePlatforms()
    {
        var playerPosition = player.transform.position;
        var down = playerPosition.y - Camera.main.orthographicSize - _halfPlatformHeight;

        while (down > _activePlatforms[0].transform.position.y)
        {
            Destroy(_activePlatforms[0].gameObject);
            _activePlatforms.RemoveAt(0);
        }
    }

    private PlatformBase GetRandomPlatformType()
    {
        var random = Random.Range(0, 10);
        return random > 5 ? (PlatformBase) orangePlatform : redPlatform;
    }

    public void Stop()
    {
        _activePlatforms = new List<PlatformBase>();
    }
}