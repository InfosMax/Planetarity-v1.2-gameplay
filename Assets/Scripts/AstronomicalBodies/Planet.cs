using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : SpaceBody
{
    protected const int MIN_SIZE = 3;

    private float orbitRotationSpeed;
    public float OrbitRotationSpeed { get => orbitRotationSpeed; set => orbitRotationSpeed = value; }
    private bool isRotationClockwise;
    public bool IsClockwise { get => isRotationClockwise; set => isRotationClockwise = value; }

    public Planet(int size, float speed)
    {
        Size = size;
        Speed = speed;
    }

    private void Start()
    {
        // Clockwise or counter clockwise? 
        IsClockwise = Random.Range(0, 2) == 0 ? true : false;
        OrbitRotationSpeed = Random.Range(15f, 45f);
        Speed = 5f;
        rotationVector = new Vector3(Random.value, Random.value, Random.value);
        InitAppearance();
    }

    private void InitAppearance()
    {
        Size = Random.Range(MIN_SIZE, Sun.Size - 2);
        transform.localScale *= Size;
        MaterialsStore materials = GameManager.Instance.PlanetMaterials;
        GetComponent<MeshRenderer>().material = GameManager.Instance.PlanetMaterials.GetRandomPlanetMaterial();
    }

    private void RotateAroundSun()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, 
            (isRotationClockwise ? OrbitRotationSpeed : -OrbitRotationSpeed) * Time.deltaTime);
    }

    private void ChangeRotationDirrection() => IsClockwise = !IsClockwise;

    protected override void Update()
    {
        RotateSelf();
        RotateAroundSun();
    }
}
