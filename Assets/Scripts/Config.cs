using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Config")]
public class Config : ScriptableObject
{
    [Header("Физика")]
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _linearDamping = 2f;
    [SerializeField] private float _angularDamping = 2f;

    [Header("Разделение")]
    [SerializeField] private int _minObjectsToSpawn = 2;
    [SerializeField] private int _maxObjectsToSpawn = 6;
    [SerializeField] private float _scaleMultiplier = 0.5f;
    [SerializeField] private float _baseSplitChance = 1f;
    [SerializeField] private float _chanceReductionFactor = 0.5f;
    [SerializeField] private int _maxSplitLevel = 10;

    [Header("Взрыв")]
    [SerializeField] private float _explosionForce = 200f;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _upwardsModifier = 0.3f;

    [Header("Начальные настройки")]
    [SerializeField] private int _initialObjectsCount = 3;
    [SerializeField] private float _initialScale = 1f;

    [Header("Спавн")]
    [SerializeField] private float _spawnVelocityRange = 1f;
    [SerializeField] private float _spawnVerticalMin = 0.5f;
    [SerializeField] private float _spawnVerticalMax = 1.5f;

    public float Mass => _mass;
    public float LinearDamping => _linearDamping;
    public float AngularDamping => _angularDamping;
    public int MinObjectsToSpawn => _minObjectsToSpawn;
    public int MaxObjectsToSpawn => _maxObjectsToSpawn;
    public float ScaleMultiplier => _scaleMultiplier;
    public float BaseSplitChance => _baseSplitChance;
    public float ChanceReductionFactor => _chanceReductionFactor;
    public int MaxSplitLevel => _maxSplitLevel;
    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;
    public float UpwardsModifier => _upwardsModifier;
    public int InitialObjectsCount => _initialObjectsCount;
    public float InitialScale => _initialScale;
    public float SpawnVelocityRange => _spawnVelocityRange;
    public float SpawnVerticalMin => _spawnVerticalMin;
    public float SpawnVerticalMax => _spawnVerticalMax;
}