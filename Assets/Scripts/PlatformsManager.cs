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
    [SerializeField] private List<PlatformBase> platfroms = new List<PlatformBase>();
    [SerializeField] public float minY = 0.8f;
    [SerializeField] public float maxY = 3f;
    [SerializeField] public Player player;
    [SerializeField] public Enemy enemy;

    private List<PlatformBase> _activePlatforms = new List<PlatformBase>();
    private float _levelWidth => Camera.main.orthographicSize * Camera.main.aspect;
    private float _halfPlatformHeight => (GetRandomPlatformType().transform.localScale.y / 2);
    private bool _active;

    public void Update()
    {
        if (_active)
        {
            TryGeneratePlatforms();
            TryRemovePlatforms();
        }
    }

    private void GenerateFirstPlatform()
    {
        //Base platform always under player
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = player.transform.position.x;
        spawnPosition.y = -Camera.main.orthographicSize + player.gameObject.transform.localScale.x * 2;
        _activePlatforms.Add(Instantiate(GetRandomPlatformType(), spawnPosition, Quaternion.identity));
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
            var platform = Instantiate(platformBase, spawnPosition, Quaternion.identity);
            _activePlatforms.Add(platform);
            // platform.InstantiateEnemy(enemy);
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
        List<PlatformBase> suitablePlatforms = GetSuitablePlatforms();
        int totalProbability = _countedProbability;

        var random = Random.Range(0, totalProbability);
        return GetPlatformWithThisProbability(suitablePlatforms, random);
    }

    private int _countedProbability;

    private List<PlatformBase> GetSuitablePlatforms()
    {
        List<PlatformBase> list = new List<PlatformBase>();
        _countedProbability = 0;

        for (int i = 0; i < platfroms.Count; i++)
        {
            if (player.MaxHeight >= platfroms[i].MinHeight)
            {
                list.Add(platfroms[i]);
                _countedProbability += platfroms[i].Probability;
            }
        }

        return list;
    }

    private PlatformBase GetPlatformWithThisProbability(List<PlatformBase> platforms, int random)
    {
        int probabilityCounter = 0;

        foreach (var platform in platforms)
        {
            probabilityCounter += platform.Probability;
            if (probabilityCounter >= random)
                return platform;
        }

        //fix for any unexpected behavior
        return platforms.Last();
    }

    public void Restart()
    {
        RemoveAllPlatforms();
        _active = false;
    }

    private void RemoveAllPlatforms()
    {
        while (_activePlatforms.Count > 0)
        {
            Destroy(_activePlatforms[0].gameObject);
            _activePlatforms.RemoveAt(0);
        }
    }

    public void Activate()
    {
        _active = true;
        GenerateFirstPlatform();
    }
}