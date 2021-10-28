using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    public interface IRocketTypeManager
    {
        RocketParameters GetRocketParameters(RocketType rocketType);
        GameObject GetRocketPrefab(RocketType rocketType);
    }
}

