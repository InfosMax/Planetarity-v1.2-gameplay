using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceBody : MonoBehaviour
{
    public int Size { get ; protected set; } 
    public float Speed { get; set; }
    

    protected float selfRotationSpeedModifier = .05f;
    protected Vector3 rotationVector = Vector3.one;

    protected void RotateSelf()
    {
        transform.Rotate(rotationVector * selfRotationSpeedModifier);
    }

    protected virtual void Update()
    {
        RotateSelf();
    }
}
