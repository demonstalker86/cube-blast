using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float SpawnOffsetRadius = 1.5f;
    private const float MinSpawnY = 0.5f;
    private const float MaxSpawnY = 5f;
    private const float MinSpawnX = -8f;
    private const float MaxSpawnX = 8f;
    private const float MinSpawnZ = -8f;
    private const float MaxSpawnZ = 8f;

    [SerializeField] private Cube _prefab;
    [SerializeField] private Config _config;

    private ColorGenerator _colorGenerator = new ColorGenerator();
    private List<Cube> _activeObjects = new List<Cube>();

    public void SpawnObjects(Vector3 position, float scale, int level, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = Random.insideUnitSphere * SpawnOffsetRadius;
            Vector3 spawnPos = position + offset;

            spawnPos.y = Mathf.Clamp(spawnPos.y, MinSpawnY, MaxSpawnY);

            spawnPos.x = Mathf.Clamp(spawnPos.x, MinSpawnX, MaxSpawnX);

            spawnPos.z = Mathf.Clamp(spawnPos.z, MinSpawnZ, MaxSpawnZ);

            Cube obj = Instantiate(_prefab, spawnPos, Quaternion.identity);

            obj.transform.localScale = Vector3.one * scale;

            Color randomColor = _colorGenerator.GetRandomColor();

            obj.Initialize(randomColor, level, _config.BaseSplitChance, _config.ChanceReductionFactor);

            obj.SetPhysics(_config.Mass, _config.LinearDamping, _config.AngularDamping);

            obj.SetRandomVelocity(_config.SpawnVelocityRange, _config.SpawnVerticalMin, _config.SpawnVerticalMax);

            _activeObjects.Add(obj);
        }
    }

    public void RemoveObject(Cube obj)
    {
        if (obj == null)
        {
            return;
        }

        _activeObjects.Remove(obj);

        Destroy(obj.gameObject);
    }

    public List<Cube> GetActiveObjects()
    {
        return _activeObjects;
    }

    private void ClearAll()
    {
        foreach (Cube obj in _activeObjects)
        {
            Destroy(obj.gameObject);
        }

        _activeObjects.Clear();
    }
}