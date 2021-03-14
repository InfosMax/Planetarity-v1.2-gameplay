using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    //Player's rocket storage.
    //Each user has all but one random type of rockets. 
    public class RocketsStorage
    {
        private Dictionary<string, int> rockets;
        public RocketsStorage()
        {
            rockets = new Dictionary<string, int>();
        }

        // Storate initialization with {rocketNames} rockets.
        // Each rocket type has randomly from {min} to {max} rockets
        public void Init(string[] rocketNames, int min, int max)
        {
            int bannedTypeIndex = Random.Range(0, rocketNames.Length);
            for(var i = 0; i < rocketNames.Length; i++)
            {
                if(bannedTypeIndex != i)
                {
                    rockets.Add(rocketNames[i], Random.Range(min, max + 1));
                }
            }
        }

        public void FillStorage(Dictionary<string, int> newRockets)
        {
            rockets = newRockets;
        }

        public bool TryGetRocket(string rocketName)
        {
            if (rockets.TryGetValue(rocketName, out int rocketsCount))
            {
                rockets[rocketName] -= 1;
                if (rockets[rocketName] <= 0)
                    rockets.Remove(rocketName);
                return true;
            }
            else
            {
                GameManagement.GameManager.Instance.ShowNotification("You don't have such a rocket! :(");
                return false;
            }
        }

        //Method for getting rockets for bots
        public string GetAnyRocket()
        {
            if(rockets.Count > 0)
            {
                var keys = new string[rockets.Count];
                rockets.Keys.CopyTo(keys, 0);
                string randomKey = keys[Random.Range(0, rockets.Count)];
                rockets[randomKey] -= 1;
                if(rockets[randomKey] <= 0)
                    rockets.Remove(randomKey);

                return randomKey;
            }
            else
            {
                return null;
            }
        }
    }
}

