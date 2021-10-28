using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.RocketsFunctionality;

namespace Planetarity.AstronomicalBodies
{
    public abstract class SpaceBody : MonoBehaviour
    {
        protected const int MinSize = 4;
        protected float selfRotationSpeedModifier = .05f;
        protected Vector3 rotationVector;

        protected int Size { get; set; }
        protected float Speed { get; set; }
        protected SphereCollider ObjectGravityZone { get; set; }

        protected void RotateSelf()
        {
            transform.Rotate(rotationVector * selfRotationSpeedModifier);
        }

        protected virtual void Update()
        {
            RotateSelf();
        }

        protected virtual void Awake()
        {
            rotationVector = new Vector3(Random.value, Random.value, Random.value);
        }

        protected virtual void Start()
        {
            AssignGravityZone();
        }

        protected void AssignGravityZone()
        {
            ObjectGravityZone = gameObject.AddComponent<SphereCollider>();
            ObjectGravityZone.isTrigger = true;
            ObjectGravityZone.radius = 1.3f;
        }

        protected virtual void InitAppearance()
        {
            Size = MinSize;
            transform.localScale *= Size;
        }

        // Space object's gravitation implementation
        protected void OnTriggerStay(Collider other)
        {
            if (other.tag == "Rocket" && other.GetComponent<Rocket>().LauncherPlanet != gameObject)
            {
                var forceVector = (transform.position - other.transform.position).normalized;
                other.attachedRigidbody.AddForce(forceVector * Size * Time.deltaTime * 1000f, ForceMode.Force);
            }
        }
    }
}
