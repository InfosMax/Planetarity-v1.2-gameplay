using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.GameManagement;

namespace Planetarity.RocketsFunctionality
{

    // Heavy rocket:
    // higher cooldown, smaller acceleration, high damage, high fuel capacity and, obviously, heavier.
    public class HeavyRocket : Rocket
    {
        protected static GameObject rocketPrefab;
        public override GameObject RocketPrefab { get => rocketPrefab; set => rocketPrefab = value; }
        protected static string rocketName = "HeavyRocketPref";
        public override string RocketName { get => rocketName; set => rocketName = value; }
        protected override void InitParameters()
        {
            base.InitParameters();

            Parameters[RocketProperties.Cooldown] = 5;
            Parameters[RocketProperties.Acceleration] = 1;

            Parameters[RocketProperties.Damage] = 5;
            Parameters[RocketProperties.FuelCapacity] = 5;
        }
    }
}
