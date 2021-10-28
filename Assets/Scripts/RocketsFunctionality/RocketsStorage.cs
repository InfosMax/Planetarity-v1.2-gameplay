using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    //Player's rocket storage.
    //Each user has all but one random type of rockets. 
    public class RocketsStorage
    {
        private Dictionary<RocketType, int> rockets;

        // Storage initialization with {rocketType} rockets.
        // Each rocket type has randomly from {min} to {max} rockets
        public RocketsStorage(int min, int max)
        {
            rockets = new Dictionary<RocketType, int>();
            var rocketTypeNames = System.Enum.GetNames(typeof(RocketType));
            int bannedTypeIndex = Random.Range(0, rocketTypeNames.Length);

            for (var i = 0; i < rocketTypeNames.Length; i++)
            {
                if (bannedTypeIndex != i)
                {
                    rockets.Add((RocketType)i, Random.Range(min, max + 1));
                }
            }
        }

        public bool TryGetRocket(RocketType rocketType)
        {
            if (rockets.TryGetValue(rocketType, out int rocketsCount))
            {
                if (rocketsCount > 0)
                {
                    rockets[rocketType]--;
                    return true;
                }
                else
                {
                    rockets.Remove(rocketType);
                    return false;
                }
            }
            else
                return false;
        }

        //Method for getting rockets for bots. Randomly gives only available rockets.
        public RocketType? GetAnyRocket()
        {
            if(rockets.Count > 0)
            {
                var keys = new RocketType[rockets.Count];
                rockets.Keys.CopyTo(keys, 0);
                RocketType randomKey = keys[Random.Range(0, rockets.Count)];

                if (TryGetRocket(randomKey))
                    return randomKey;
                else
                    return GetAnyRocket();
            }
            else
            {
                return null;
            }
        }

        public RocketType[] GetAvailableRocketTypes()
        {
            var result = new RocketType[rockets.Count];
            rockets.Keys.CopyTo(result, 0);
            return result;
        }
    }
}

