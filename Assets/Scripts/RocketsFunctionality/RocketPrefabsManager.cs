using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Planetarity.RocketsFunctionality
{
    public class RocketPrefabsManager : IRocketPrefabManager
    {
        private Dictionary<String, (GameObject, Texture2D)> rocketsResources;

        public RocketPrefabsManager()
        {
            rocketsResources = new Dictionary<string, (GameObject, Texture2D)>();
        }

        public void AddRocket(KeyValuePair<String, (GameObject, Texture2D)> rocket)
        {
            if (rocketsResources.ContainsKey(rocket.Key))
            {
                Debug.Log($"Rocket {rocket.Key} already exists in the store");
                return;
            }
            else
            {
                rocketsResources.Add(rocket.Key, rocket.Value);
            }  
        }

        public (GameObject, Texture2D) GetRocketResources(string rocketName)
        {
            Debug.Log(rocketsResources.Count);
            foreach (KeyValuePair<String, (GameObject, Texture2D)> one in rocketsResources)
            {
                Debug.Log(one.Key);
            }
            rocketsResources.TryGetValue(rocketName, out (GameObject, Texture2D) rocketResource);
            return rocketResource;
        }

    }
}

