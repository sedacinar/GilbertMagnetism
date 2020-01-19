using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
   
    public enum Pole
        {
            NorthPole,
            SouthPole,
            Iron
        }

        public float MagnetForce;
        public Pole MagneticPole;
        public Rigidbody RigidBody;
       
 }
