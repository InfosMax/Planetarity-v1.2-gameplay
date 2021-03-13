using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    public interface IRocketPrefabManager
    {
        void AddRocket(KeyValuePair<string, GameObject> rocket);
        GameObject GetRocketPrefab(string rocketName);
    }
}

