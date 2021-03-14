using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.PlayerFunctionality;

namespace Planetarity.RocketsFunctionality
{
    public interface IRocketLaunchStation
    {
        Rocket InitiateLaunch(Player initiator, string rocketName, Vector3 dirrection);
    }
}


