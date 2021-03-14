using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Planetarity.PlayerFunctionality;

namespace Planetarity.RocketsFunctionality
{
    public abstract class Rocket : MonoBehaviour, IRocket
    {
        private GameObject launcherPlanet;
        private Rigidbody rigidBody = null;
        public abstract string RocketName { get; set; }
        public abstract GameObject RocketPrefab { get; set; }
        public abstract Texture2D RocketImg { get; set; }

        public const float STANDARD_PROPERTY_VALUE = 3f;
        protected Dictionary<RocketProperties, float> parameters = new Dictionary<RocketProperties, float>();
        public Dictionary<RocketProperties, float> Parameters => parameters;

        public GameObject LauncherPlanet { get => launcherPlanet; set => launcherPlanet = value; }

        protected virtual void Awake()
        {
            InitParameters();
            loadResources();
            GameManagement.GameManager.Instance.AddRocket(new KeyValuePair<string, (GameObject, Texture2D)>(RocketName, (RocketPrefab, RocketImg)));
        }

        protected virtual void Start()
        {
            PostInitCalculations();
            Destroy(this, 10f);
        }

        protected void loadResources()
        {
            if(RocketName != null && RocketName != string.Empty)
            {
                RocketPrefab = Resources.Load<GameObject>("Prefabs/Rockets/" + RocketName);
                RocketImg = Resources.Load<Texture2D>("Images/Rockets/" + RocketName);
            }
            else
            {
                Debug.Log("Specify rocket's name for getting it's prefab!");
            }

        }

        protected virtual void InitParameters()
        {
            int RocketPropertiesCount = Enum.GetNames(typeof(RocketProperties)).Length;
            for (var i = 0; i < RocketPropertiesCount; i++)
            {
                Parameters.Add((RocketProperties)i, STANDARD_PROPERTY_VALUE);
            }
        }

        protected virtual void PostInitCalculations()
        {
            // Rocket's weight equals to doubled FuelCapacity
            Parameters[RocketProperties.Weight] = Parameters[RocketProperties.FuelCapacity] * 2;
            rigidBody = GetComponent<Rigidbody>();
            if (rigidBody)
            {
                rigidBody.mass = Parameters[RocketProperties.Weight];
            }
                
        }

        public void Fly()
        {
            if (rigidBody)
            {
                if(Parameters[RocketProperties.FuelCapacity] > 0)
                {
                    rigidBody.AddForce(Vector3.up * Parameters[RocketProperties.Acceleration] * Time.deltaTime * 20f, ForceMode.Force);
                    Parameters[RocketProperties.FuelCapacity] -= Time.deltaTime;
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

        private void Update()
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
}

