using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    private const float MinSpawnX = -3f;
    private const float MaxSpawnX = 3f;
    private const float MinSpawnY = 1f;
    private const float MaxSpawnY = 4f;
    private const float MinSpawnZ = -3f;
    private const float MaxSpawnZ = 3f;
    private const int InitialLevel = 1;
    private const int MaxExclusiveOffset = 1;
    private const int LevelIncrement = 1;
    private const int InitialObjectsCountPerSpawn = 1;

    [SerializeField] private Config _config;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Raycaster _raycaster;

    private void OnEnable()
    {
        _raycaster.ObjectHit += OnObjectHit;
    }

    private void OnDisable()
    {
        _raycaster.ObjectHit -= OnObjectHit;
    }

    private void Start()
    {
        for (int i = 0; i < _config.InitialObjectsCount; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(MinSpawnX, MaxSpawnX),

                Random.Range(MinSpawnY, MaxSpawnY),

                Random.Range(MinSpawnZ, MaxSpawnZ)
            );

            _spawner.SpawnObjects(position, _config.InitialScale, InitialLevel, InitialObjectsCountPerSpawn);
        }
    }

    private void OnObjectHit(Cube obj)
    {
        if (obj == null)
        {
            return;
        }

        Vector3 center = obj.transform.position;
        float currentScale = obj.transform.localScale.x;
        int currentLevel = obj.SplitLevel;

        _spawner.RemoveObject(obj);

        if (Random.value <= obj.SplitChance && currentLevel < _config.MaxSplitLevel)
        {
            int count = Random.Range(_config.MinObjectsToSpawn, _config.MaxObjectsToSpawn + MaxExclusiveOffset);

            float newScale = currentScale * _config.ScaleMultiplier;
            int newLevel = currentLevel + LevelIncrement;

            _spawner.SpawnObjects(center, newScale, newLevel, count);
            
            List<Cube> newObjects = _spawner.GetActiveObjects();

            _exploder.ApplyExplosion(center, newObjects, _config.ExplosionForce, _config.ExplosionRadius, _config.UpwardsModifier);
        }
    }
}