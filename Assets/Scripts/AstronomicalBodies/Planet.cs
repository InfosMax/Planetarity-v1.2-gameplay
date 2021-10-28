using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Planetarity.Utility;

namespace Planetarity.AstronomicalBodies
{
    public class Planet : SpaceBody
    {
        private const float DefaultSpeed = 5f;
        private const float DefaultRotationSpeed = 10f;
        private float orbitRotationSpeed;
        private bool isRotationClockwise;
        private bool IsClockwise { get => isRotationClockwise; set => isRotationClockwise = value; }

        protected override void Awake()
        {
            base.Awake();
            // Clockwise or counter clockwise? 
            IsClockwise = Random.Range(0, 2) == 0 ? true : false;
            orbitRotationSpeed = Random.Range(DefaultRotationSpeed, 2.5f * DefaultRotationSpeed);
            Speed = DefaultSpeed;
            InitAppearance();
        }

        protected override void InitAppearance()
        {
            Size = Random.Range(MinSize, Sun.ComparingSize - 2);
            transform.localScale *= Size;
            GetComponent<MeshRenderer>().material = GameManagement.GameManager.Instance.PlanetMaterials.GetRandomPlanetMaterial();
        }

        private void RotateAroundSun()
        {
            transform.RotateAround(Vector3.zero, Vector3.forward,
                (isRotationClockwise ? orbitRotationSpeed : -orbitRotationSpeed) * Time.deltaTime);
        }

        private void ChangeRotationDirrection() => IsClockwise = !IsClockwise;

        protected override void Update()
        {
            RotateSelf();
            RotateAroundSun();
        }
    }
}
