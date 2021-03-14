using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    public interface IRocketPrefabManager
    {
        (GameObject, Texture2D) GetRocketResources(string rocketName);
        GameObject GetRocketPrefab(string rocketName);
        string[] GetRocketsNames();
    }
}

