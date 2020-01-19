using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MagneticForce : MonoBehaviour
{
    public float Permeability = 0.05f;
    public float MaxForce = 10000.0f;

    void FixedUpdate()
    {
        Magnet[] magnets = FindObjectsOfType<Magnet>();
        int magCount = magnets.Length;

        for (int i = 0; i < magCount; i++)
        {
            Magnet m1 = magnets[i];
            if (m1.RigidBody == null)
                continue;

            Rigidbody rb1 = m1.RigidBody;
            Vector3 initialForceF1 = Vector3.zero;
            Vector3 initialForceF2 = Vector3.zero;
            for (int j = 0; j < magCount; j++)
            {
                if (i == j)
                    continue;

                Magnet m2 = magnets[j];

                if (m2.MagnetForce < 5.0f)
                    continue;

                if (m1.transform.parent == m2.transform.parent)
                    continue;

                Vector3 forceValue = GilbertForce(m1, m2);
                float magnetForce = m1.MagnetForce * m2.MagnetForce;

                initialForceF1 += forceValue * magnetForce;
            }

            if (initialForceF1.magnitude > MaxForce)
            {
                initialForceF1 = initialForceF1.normalized * MaxForce;
            }
            rb1.AddForceAtPosition(initialForceF1, m1.transform.position);
        }
    }
    Vector3 GilbertForce(Magnet magnet1, Magnet magnet2)
    {
        Vector3 m1 = magnet1.transform.position;
        Vector3 m2 = magnet2.transform.position;
        Vector3 difference = m2 - m1;
        float dist = difference.magnitude;
        float divided = Permeability * magnet1.MagnetForce * magnet2.MagnetForce;
        float divisor = 4 * Mathf.PI * dist;

        var result = (divided / divisor);

        if (magnet1.MagneticPole == magnet2.MagneticPole)
            result = -result;

        return result * difference.normalized;
    }
}
