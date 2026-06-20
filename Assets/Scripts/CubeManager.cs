using UnityEngine;
using System.Collections.Generic;

public class CubeManager : MonoBehaviour
{
    private const float DefaultMass = 1f;
    private const float DefaultLinearDamping = 0.5f;
    private const float DefaultAngularDamping = 0.5f;
    private const int DefaultMinCubesToSpawn = 2;
    private const int DefaultMaxCubesToSpawn = 6;
    private const float DefaultScaleMultiplier = 0.5f;
    private const float DefaultBaseSplitChance = 1f;
    private const float DefaultChanceReductionFactor = 0.5f;
    private const int DefaultMaxSplitLevel = 10;
    private const float DefaultExplosionForce = 500f;
    private const float DefaultExplosionRadius = 5f;
    private const float DefaultUpwardsModifier = 1f;
    private const int DefaultInitialCubesCount = 3;
    private const float DefaultInitialScale = 1f;
    private const float DefaultSpawnHeightMin = 1f;
    private const float DefaultSpawnHeightMax = 4f;
    private const float DefaultSpawnHorizontalMin = -3f;
    private const float DefaultSpawnHorizontalMax = 3f;
    private const float DefaultSpawnVelocityRange = 1f;
    private const float DefaultSpawnVerticalMin = 0.5f;
    private const float DefaultSpawnVerticalMax = 1.5f;
    private const int LevelOne = 1;
    private const int MaxAlpha = 1;
    private const float MinColorValue = 0f;
    private const float MaxColorValue = 1f;
    private const float SpawnOffsetRadius = 1.5f;
    private const float MinSpawnY = 0.5f;
    private const float MaxSpawnY = 5f;
    private const float MinSpawnX = -8f;
    private const float MaxSpawnX = 8f;
    private const float MinSpawnZ = -8f;
    private const float MaxSpawnZ = 8f;
    private const int MaxExclusiveOffset = 1;
    private const int LevelIncrement = 1;   

    [Header("Префаб")]
    [SerializeField] private GameObject _cubePrefab;

    [Header("Настройки физики")]
    [SerializeField] private float _mass = DefaultMass;
    [SerializeField] private float _linearDamping = DefaultLinearDamping;
    [SerializeField] private float _angularDamping = DefaultAngularDamping;

    [Header("Настройки разделения")]
    [SerializeField] private int _minCubesToSpawn = DefaultMinCubesToSpawn;
    [SerializeField] private int _maxCubesToSpawn = DefaultMaxCubesToSpawn;
    [SerializeField] private float _scaleMultiplier = DefaultScaleMultiplier;
    [SerializeField] private float _baseSplitChance = DefaultBaseSplitChance;
    [SerializeField] private float _chanceReductionFactor = DefaultChanceReductionFactor;
    [SerializeField] private int _maxSplitLevel = DefaultMaxSplitLevel;

    [Header("Настройки взрыва")]
    [SerializeField] private float _explosionForce = DefaultExplosionForce;
    [SerializeField] private float _explosionRadius = DefaultExplosionRadius;
    [SerializeField] private float _upwardsModifier = DefaultUpwardsModifier;

    [Header("Начальные настройки")]
    [SerializeField] private int _initialCubesCount = DefaultInitialCubesCount;
    [SerializeField] private float _initialScale = DefaultInitialScale;

    [Header("Настройки спавна")]
    [SerializeField] private Vector2 _spawnHeightRange = new Vector2(DefaultSpawnHeightMin, DefaultSpawnHeightMax);
    [SerializeField] private Vector2 _spawnHorizontalRange = new Vector2(DefaultSpawnHorizontalMin, DefaultSpawnHorizontalMax);
    [SerializeField] private float _spawnVelocityRange = DefaultSpawnVelocityRange;
    [SerializeField] private float _spawnVerticalMin = DefaultSpawnVerticalMin;
    [SerializeField] private float _spawnVerticalMax = DefaultSpawnVerticalMax;

    private List<GameObject> _activeCubes = new List<GameObject>();

    private void Start()
    {
        CreateInitialCubes();
    }

    private void OnDisable()
    {
        ClearAllCubes();
    }

    public void Restart()
    {
        ClearAllCubes();

        CreateInitialCubes();
    }

    public int GetActiveCubesCount()
    {
        return _activeCubes.Count;
    }

    private void OnCubeClicked(CubeController controller)
    {
        if (controller == null) return;

        Vector3 explosionCenter = controller.transform.position;
        float currentScale = controller.transform.localScale.x;
        int currentLevel = controller.SplitLevel;
        float chance = controller.SplitChance;

        RemoveCube(controller.gameObject);

        if (ShouldSplit(chance, currentLevel))
        {
            SpawnChildCubes(explosionCenter, currentScale, currentLevel, out List<GameObject> newCubes);

            ApplyExplosion(explosionCenter, newCubes);
        }
    }

    private bool ShouldSplit(float chance, int currentLevel)
    {
        return Random.value <= chance && currentLevel < _maxSplitLevel;
    }

    private void SpawnChildCubes(Vector3 position, float currentScale, int currentLevel, out List<GameObject> newCubes)
    {
        newCubes = new List<GameObject>();

        int count = Random.Range(_minCubesToSpawn, _maxCubesToSpawn + MaxExclusiveOffset);

        float newScale = currentScale * _scaleMultiplier;
        int newLevel = currentLevel + LevelIncrement;

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * SpawnOffsetRadius;
            Vector3 spawnPosition = position + randomOffset;

            spawnPosition.y = Mathf.Clamp(spawnPosition.y, MinSpawnY, MaxSpawnY);

            spawnPosition.x = Mathf.Clamp(spawnPosition.x, MinSpawnX, MaxSpawnX);

            spawnPosition.z = Mathf.Clamp(spawnPosition.z, MinSpawnZ, MaxSpawnZ);

            GameObject cube = CreateCube(spawnPosition, newScale, newLevel);

            if (cube != null) newCubes.Add(cube);
        }
    }

    private GameObject CreateCube(Vector3 position, float scale, int level)
    {
        if (_cubePrefab == null)
        {
            Debug.LogError("Cube prefab not assigned in CubeManager!");

            return null;
        }

        GameObject cube = Instantiate(_cubePrefab, position, Quaternion.identity);

        cube.transform.localScale = Vector3.one * scale;
        cube.transform.rotation = Quaternion.identity;

        CubeController controller = cube.GetComponent<CubeController>();

        if (controller != null)
        {
            Color randomColor = GetRandomColor();

            controller.Initialize(randomColor, level, _baseSplitChance, _chanceReductionFactor);

            controller.SetPhysics(_mass, _linearDamping, _angularDamping);

            controller.Clicked += OnCubeClicked;

            controller.AddRandomVelocity(_spawnVelocityRange, _spawnVerticalMin, _spawnVerticalMax);
        }
        else
        {
            Debug.LogWarning("CubeController component not found on cube prefab!");
        }

        _activeCubes.Add(cube);

        return cube;
    }

    private Color GetRandomColor()
    {
        return new Color(
            Random.Range(MinColorValue, MaxColorValue),

            Random.Range(MinColorValue, MaxColorValue),

            Random.Range(MinColorValue, MaxColorValue),

            MaxAlpha
        );
    }

    private void ApplyExplosion(Vector3 center, List<GameObject> cubes)
    {
        foreach (GameObject cube in cubes)
        {
            if (cube == null) continue;

            CubeController controller = cube.GetComponent<CubeController>();

            if (controller != null)
            {
                controller.AddExplosionForce(
                    center,
                    _explosionForce,
                    _explosionRadius,
                    _upwardsModifier
                );
            }
        }
    }

    private void RemoveCube(GameObject cube)
    {
        if (cube == null) return;

        CubeController controller = cube.GetComponent<CubeController>();

        if (controller != null)
        {
            controller.Clicked -= OnCubeClicked;
        }

        _activeCubes.Remove(cube);

        Destroy(cube);
    }

    private void CreateInitialCubes()
    {
        for (int i = 0; i < _initialCubesCount; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(_spawnHorizontalRange.x, _spawnHorizontalRange.y),

                Random.Range(_spawnHeightRange.x, _spawnHeightRange.y),

                Random.Range(_spawnHorizontalRange.x, _spawnHorizontalRange.y)
            );

            CreateCube(position, _initialScale, LevelOne);
        }
    }

    private void ClearAllCubes()
    {
        foreach (GameObject cube in _activeCubes)
        {
            if (cube != null)
            {
                CubeController controller = cube.GetComponent<CubeController>();

                if (controller != null)
                {
                    controller.Clicked -= OnCubeClicked;
                }

                Destroy(cube);
            }
        }

        _activeCubes.Clear();
    }
}