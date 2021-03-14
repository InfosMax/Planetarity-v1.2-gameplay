using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Planetarity.RocketsFunctionality
{
    public class RocketPrefabsManager : IRocketPrefabManager
    {
        private static Dictionary<string, (GameObject, Texture2D)> rocketsResources;

        static RocketPrefabsManager()
        {
            rocketsResources = new Dictionary<string, (GameObject, Texture2D)>();
        }

        public static void AddRocket(KeyValuePair<string, (GameObject, Texture2D)> rocket)
        {
            if (rocketsResources.ContainsKey(rocket.Key))
            {
                return;
            }
            else
            {
                rocketsResources.Add(rocket.Key, rocket.Value);
            }  
        }

        public (GameObject, Texture2D) GetRocketResources(string rocketName)
        {
            if (rocketsResources.TryGetValue(rocketName, out (GameObject, Texture2D) rocketResource))
            {
                return rocketResource;
            }
            else
            {
                Debug.Log("Such a rocket doesn't exist!");
                return (null, null);
            }
        }

        public GameObject GetRocketPrefab(string rocketName)
        {
            if (rocketsResources.TryGetValue(rocketName, out (GameObject, Texture2D) rocketPrefab))
            {
                return rocketPrefab.Item1;
            }
            else
            {
                Debug.Log("Such a rocket doesn't exist!");
                return null;
            }
        }

        public string[] GetRocketsNames()
        {
            var rocketsResourcesKeys = new string[rocketsResources.Count];
            rocketsResources.Keys.CopyTo(rocketsResourcesKeys, 0);
            return rocketsResourcesKeys;
        }

    }
}

