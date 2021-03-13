using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Planetarity.GameManagement;

namespace Planetarity.RocketsFunctionality
{

// Short disnace rocket:
// Low fuel, higher Dmg, small cooldown.
public class ShortRocket : Rocket
    {
        protected static GameObject rocketPrefab;
        public override GameObject RocketPrefab { get => rocketPrefab; set => rocketPrefab = value; }
        protected static string rocketName = "ShortRocketPref";
        public override string RocketName { get => rocketName; set => rocketName = value; }

        protected override void InitParameters()
        {
            base.InitParameters();

            Parameters[RocketProperties.FuelCapacity] = 1;

            Parameters[RocketProperties.Damage] = 4;
            Parameters[RocketProperties.Cooldown] = 2;
        }
    }
}


