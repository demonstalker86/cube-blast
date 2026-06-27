using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private const float MaxForceMultiplier = 1f;

    public void ApplyExplosion(Vector3 center, List<Cube> objects, float force, float radius, float upwards)
    {
        foreach (Cube obj in objects)
        {
            if (obj != null)
            {
                float distance = Vector3.Distance(center, obj.transform.position);

                if (distance < radius)
                {
                    float forceMultiplier = MaxForceMultiplier - (distance / radius);
                    float finalForce = force * forceMultiplier;

                    obj.AddExplosionForce(center, finalForce, radius, upwards);
                }
            }
        }
    }
}