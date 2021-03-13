using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    public interface IRocketPrefabManager
    {
        void AddRocket(KeyValuePair<string, (GameObject, Texture2D)> rocket);
        (GameObject, Texture2D) GetRocketResources(string rocketName);
    }
}

