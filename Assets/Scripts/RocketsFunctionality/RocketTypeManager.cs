using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Planetarity.RocketsFunctionality
{
    public class RocketTypeManager : MonoBehaviour, IRocketTypeManager
    {
        [SerializeField]
        private List<RocketParameters> rocketTypes;

        public RocketParameters GetRocketParameters(RocketType rocketType)
        {
            return rocketTypes.Find(parameters => parameters.rocketType == rocketType);
        }

        public GameObject GetRocketPrefab(RocketType rocketType)
        {
            return rocketTypes.Find(parameters => parameters.rocketType == rocketType).prefab;
        }
    }
}

