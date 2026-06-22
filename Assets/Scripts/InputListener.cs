using UnityEngine;

public class InputListener : MonoBehaviour
{
    private const int PrimaryInputIndex = 0;
    private const int NoTouches = 0;

    public event System.Action<Vector2> Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(PrimaryInputIndex))
        {
            Clicked?.Invoke(Input.mousePosition);
        }

        if (Input.touchCount > NoTouches)
        {
            Touch touch = Input.GetTouch(PrimaryInputIndex);

            if (touch.phase == TouchPhase.Began)
            {
                Clicked?.Invoke(touch.position);
            }
        }
    }
}