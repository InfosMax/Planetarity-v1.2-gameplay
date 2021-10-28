using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Planetarity.PlayerFunctionality;

namespace Planetarity.RocketsFunctionality
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour, IRocket
    {
        protected Rigidbody rigidBody;
        protected const float STANDARD_PROPERTY_VALUE = 3f;

        public Dictionary<RocketProperties, float> Parameters { get; set; } = new Dictionary<RocketProperties, float>();
        public GameObject LauncherPlanet { get; set; }


        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            InitParameters();
        }

        protected virtual void Start()
        {
            PostInitCalculations();
            Destroy(gameObject, 10f);
        }

        protected virtual void InitParameters()
        {
            int RocketPropertiesCount = Enum.GetNames(typeof(RocketProperties)).Length;
            for (var i = 0; i < RocketPropertiesCount; i++)
            {
                Parameters.Add((RocketProperties)i, STANDARD_PROPERTY_VALUE);
            }
        }

        //need decorator
        public void OverwriteProperties(RocketParameters parameters)
        {
            if (!parameters)
                return;

            //Set available properies values
            foreach (RocketPropertyEntry entry in parameters.properties)
            {
                Parameters[entry.property] = entry.value;
            }
        }

        protected virtual void PostInitCalculations()
        {
            // Rocket's weight equals to double FuelCapacity
            Parameters[RocketProperties.Weight] = Parameters[RocketProperties.FuelCapacity] * 2f;
            Parameters[RocketProperties.Damage] *= 2f;
            Parameters[RocketProperties.Acceleration] *= 5f;

            rigidBody.mass = Parameters[RocketProperties.Weight];
        }

        public void Fly()
        {
            if (rigidBody)
            {
                if(Parameters[RocketProperties.FuelCapacity] > 0f)
                {
                    rigidBody.AddForce(transform.forward * Parameters[RocketProperties.Acceleration] * Time.fixedDeltaTime * 300f, ForceMode.Force);
                    Parameters[RocketProperties.FuelCapacity] -= Time.fixedDeltaTime;
                }
            }
        }

        public float GetRocketCooldown()
        {
            return Parameters[RocketProperties.Cooldown];
        }

        public float CalculateDamage()
        {
            Parameters.TryGetValue(RocketProperties.Damage, out float Damage);
            return Damage;
        }

        private void FixedUpdate()
        {
            Fly();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (LauncherPlanet == collision.gameObject)
                return;

            Player collisionPlayer = collision.gameObject.GetComponent<Player>();
            if (collisionPlayer != null)
                collisionPlayer.GetDamage(Parameters[RocketProperties.Damage]);

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CapsuleCollider>().enabled = false;


            GetComponent<ParticleSystem>().Play();
            Destroy(gameObject, 4f);
        }
    }

    [Serializable]
    public enum RocketType
    {
        Fast, Short, Heavy, Easy
    }
}

