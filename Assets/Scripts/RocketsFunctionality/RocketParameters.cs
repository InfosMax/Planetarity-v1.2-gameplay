using Planetarity.RocketsFunctionality;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    [CreateAssetMenu(fileName = "NewRocketParameters", menuName = "Rocket")]
    public class RocketParameters : ScriptableObject
    {
        public RocketType rocketType;
        public Sprite picture;
        public GameObject prefab;
        public RocketPropertyEntry[] properties;
    }
}

