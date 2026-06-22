using UnityEngine;

public class ColorGenerator
{
    private const float MinColorValue = 0f;
    private const float MaxColorValue = 1f;
    private const int MaxAlpha = 1;

    public Color GetRandomColor()
    {
        return new Color(
            Random.Range(MinColorValue, MaxColorValue),

            Random.Range(MinColorValue, MaxColorValue),

            Random.Range(MinColorValue, MaxColorValue),

            MaxAlpha
        );
    }
}