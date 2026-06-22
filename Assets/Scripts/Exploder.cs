using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void ApplyExplosion(Vector3 center, List<Cube> objects, float force, float radius, float upwards)
    {
        foreach (Cube obj in objects)
        {
            if (obj != null)
            {
                obj.AddExplosionForce(center, force, radius, upwards);
            }
        }
    }
}