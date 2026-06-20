using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer), typeof(Collider))]
public class CubeController : MonoBehaviour
{  
    private const int LevelOffsetForChance = 1;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event System.Action<CubeController> Clicked;

    public int SplitLevel { get; private set; }
    public float SplitChance { get; private set; }
    public Color CubeColor { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _renderer = GetComponent<Renderer>();

        if (_rigidbody != null)
        {
            _rigidbody.useGravity = true;
        }
    }

    private void Update()
    {
        HandleInput();
    }

    public void Initialize(Color color, int level, float baseChance, float chanceReduction)
    {
        CubeColor = color;
        SplitLevel = level;

        SplitChance = baseChance * Mathf.Pow(chanceReduction, level - LevelOffsetForChance);

        if (_renderer != null)
        {
            _renderer.material.color = color;
        }
    }

    public void SetPhysics(float mass, float linearDamping, float angularDamping)
    {
        if (_rigidbody == null) return;

        _rigidbody.mass = mass;
        _rigidbody.linearDamping = linearDamping;
        _rigidbody.angularDamping = angularDamping;
    }

    public void AddExplosionForce(Vector3 center, float force, float radius, float upwards)
    {
        if (_rigidbody == null) return;

        _rigidbody.AddExplosionForce(force, center, radius, upwards, ForceMode.Impulse);
    }

    public void AddRandomVelocity(float range, float verticalMin, float verticalMax)
    {
        if (_rigidbody == null) return;

        Vector3 randomVelocity = new Vector3(
            Random.Range(-range, range),

            Random.Range(verticalMin, verticalMax),

            Random.Range(-range, range)
        );

        _rigidbody.linearVelocity = randomVelocity;
    }

    private void HandleInput()
    {
        Vector3? inputPosition = InputHandler.GetInputPosition();

        if (inputPosition.HasValue)
        {           
            GameObject clickedObject = InputHandler.GetClickedObject(inputPosition.Value);

            if (clickedObject == gameObject)
            {              
                Clicked?.Invoke(this);
            }            
        }
    }
}