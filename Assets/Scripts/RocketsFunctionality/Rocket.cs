using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    public abstract class Rocket : MonoBehaviour, IRocket
    {
        public abstract string RocketName { get; set; }
        public abstract GameObject RocketPrefab { get; set; }

        public const int STANDARD_PROPERTY_VALUE = 3;
        protected readonly Dictionary<RocketProperties, int> parameters = new Dictionary<RocketProperties, int>();
        public Dictionary<RocketProperties, int> Parameters => parameters;

        protected virtual void Awake()
        {
            InitParameters();
            SetRocketPrefab();
        }

        protected virtual void Start()
        {
            PostInitCalculations();
        }

        protected void SetRocketPrefab()
        {
            if(RocketName != null && RocketName != string.Empty)
            {
                RocketPrefab = Resources.Load<GameObject>("Prefabs/Rockets/" + RocketName);
                Debug.Log("Loaded " + RocketPrefab.name);
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
        }

        public void Launch(Vector3 dirrection)
        {
            //event rocket launched
        }

        public int CalculateDamage()
        {
            Parameters.TryGetValue(RocketProperties.Damage, out int Damage);
            return Damage;
        }
    }
}

