using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : SpaceBody
{
    public new static int Size { get; private set; }

    private void Awake()
    {
        Speed = 0f;
        rotationVector = new Vector3(Random.value, Random.value, Random.value);
        InitAppearance();
    }

    private void InitAppearance()
    {
        Size = Random.Range(10, 15);
        transform.localScale *= Size;
    }
}
