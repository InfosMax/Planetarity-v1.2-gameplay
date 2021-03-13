using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Planetarity.RocketsFunctionality
{
    public class RocketPrefabsManager : IRocketPrefabManager
    {
        private Dictionary<String, GameObject> rocketsPrefabs;

        public RocketPrefabsManager()
        {
            rocketsPrefabs = new Dictionary<string, GameObject>();
        }

        public void AddRocket(KeyValuePair<String, GameObject> rocket)
        {
            if (rocketsPrefabs.ContainsKey(rocket.Key))
            {
                Debug.Log($"Rocket {rocket.Key} already exists in the store");
                return;
            }
            else
            {
                rocketsPrefabs.Add(rocket.Key, rocket.Value);
            }  
        }

        public GameObject GetRocketPrefab(string rocketName)
        {
            rocketsPrefabs.TryGetValue(rocketName, out GameObject rocketPrefab);
            return rocketPrefab;
        }

    }
}

