using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public event System.Action<Cube> ObjectHit;

    [SerializeField] private InputListener _inputListener;
    [SerializeField] private LayerMask _layerMask;

    private void OnEnable()
    {
        _inputListener.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _inputListener.Clicked -= OnClicked;
    }

    private void OnClicked(Vector2 screenPosition)
    {
        if (Camera.main == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            if (hit.collider.TryGetComponent<Cube>(out Cube obj))
            {
                ObjectHit?.Invoke(obj);
            }
        }
    }
}