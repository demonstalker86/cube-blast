using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private const int NoTouches = 0;

    [SerializeField] private int _primaryInputIndex = 0;    

    private static InputHandler _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static Vector3? GetInputPosition()
    {
        if (_instance == null)
        {
            Debug.LogError("InputHandler instance not found!");

            return null;
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(_instance._primaryInputIndex))
        {
            return Input.mousePosition;
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > NoTouches)
        {
            Touch touch = Input.GetTouch(_instance._primaryInputIndex);

            if (touch.phase == TouchPhase.Began)
            {
                return touch.position;
            }
        }
#endif

        return null;
    }

    public static GameObject GetClickedObject(Vector3 screenPosition)
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found!");

            return null;
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (RaycastHit hit in hits)
        {
            if (IsPointerOverUI() == false)
            {
                GameObject gameObject = hit.collider.gameObject;

                if (gameObject.GetComponent<CubeController>() != null)
                {
                    Debug.Log($"Hit: {gameObject.name}");

                    return gameObject;
                }
            }
        }

        Debug.Log("No CubeController found in raycast hits");

        return null;
    }

    private static bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}