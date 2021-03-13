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
        protected static string rocketName = "FastRocket";
        protected static Texture2D rocketImg;
        public override Texture2D RocketImg { get => rocketImg; set => rocketImg = value; }
        public override string RocketName { get => rocketName; set => rocketName = value; }

        protected override void InitParameters()
        {
            base.InitParameters();

            Parameters[RocketProperties.Acceleration] = 5;
            Parameters[RocketProperties.FuelCapacity] = 2;
        }

    }
}
