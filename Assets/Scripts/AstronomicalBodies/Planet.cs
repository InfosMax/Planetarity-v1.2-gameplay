using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Planetarity.Utility;

namespace Planetarity.AstronomicalBodies
{
    public class Planet : SpaceBody
    {
        private float orbitRotationSpeed;
        public float OrbitRotationSpeed { get => orbitRotationSpeed; set => orbitRotationSpeed = value; }
        private bool isRotationClockwise;
        public bool IsClockwise { get => isRotationClockwise; set => isRotationClockwise = value; }

        public Planet(int size, float speed)
        {
            Size = size;
            Speed = speed;
        }

        protected override void Awake()
        {
            base.Awake();
            // Clockwise or counter clockwise? 
            IsClockwise = Random.Range(0, 2) == 0 ? true : false;
            OrbitRotationSpeed = Random.Range(15f, 45f);
            Speed = 5f;
            InitAppearance();
        }

        protected override void InitAppearance()
        {
            Size = Random.Range(MIN_SIZE, Sun.Size - 2);
            transform.localScale *= Size;
            GetComponent<MeshRenderer>().material = MaterialsStore.Instance.GetRandomPlanetMaterial();
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
}
