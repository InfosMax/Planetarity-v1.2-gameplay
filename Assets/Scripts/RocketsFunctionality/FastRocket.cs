using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.GameManagement;

namespace Planetarity.RocketsFunctionality
{

    // Fast rocket:
    // High accceleration, smaller FuelCapacity,  other - standard.
    public class FastRocket : Rocket
    {
        protected static GameObject rocketPrefab;
        public override GameObject RocketPrefab { get => rocketPrefab; set => rocketPrefab = value; }
        protected static string rocketName = "FastRocketPref";
        public override string RocketName { get => rocketName; set => rocketName = value; }

        protected override void InitParameters()
        {
            base.InitParameters();

            Parameters[RocketProperties.Acceleration] = 5;
            Parameters[RocketProperties.FuelCapacity] = 2;
        }

    }
}
