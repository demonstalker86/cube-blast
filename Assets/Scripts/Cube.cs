using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private const int LevelOffsetForChance = 1; 
    private const float MaxVelocity = 15f;

    private int _splitLevel;
    private float _splitChance;
    private Color _cubeColor;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public int SplitLevel => _splitLevel;
    public float SplitChance => _splitChance;
    public Color CubeColor => _cubeColor;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _renderer = GetComponent<Renderer>();

        _rigidbody.useGravity = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {       
        LimitVelocity();
    }

    public void Initialize(Color color, int level, float baseChance, float chanceReduction)
    {
        _cubeColor = color;
        _splitLevel = level;

        _splitChance = baseChance * Mathf.Pow(chanceReduction, level - LevelOffsetForChance);

        if (_renderer != null)
        {
            _renderer.material.color = color;
        }
    }

    public void SetPhysics(float mass, float linearDamping, float angularDamping)
    {
        _rigidbody.mass = mass;
        _rigidbody.linearDamping = linearDamping;
        _rigidbody.angularDamping = angularDamping;
    }

    public void AddExplosionForce(Vector3 center, float force, float radius, float upwards)
    {
        _rigidbody.AddExplosionForce(force, center, radius, upwards, ForceMode.Impulse);
    }

    public void SetRandomVelocity(float range, float verticalMin, float verticalMax)
    {
        _rigidbody.linearVelocity = new Vector3(
            Random.Range(-range, range),

            Random.Range(verticalMin, verticalMax),

            Random.Range(-range, range)
        );
    }   

    private void LimitVelocity()
    {
        if (_rigidbody.linearVelocity.magnitude > MaxVelocity)
        {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * MaxVelocity;
        }
    }
}