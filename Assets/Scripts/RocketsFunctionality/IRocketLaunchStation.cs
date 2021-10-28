using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.PlayerFunctionality;

namespace Planetarity.RocketsFunctionality
{
    public interface IRocketLaunchSystem
    {
        Rocket InitiateLaunch(Player initiator, RocketType rocketName, Vector3 dirrection);
    }
}


