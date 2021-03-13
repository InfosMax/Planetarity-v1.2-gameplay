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
        protected static string rocketName = "HeavyRocket";
        protected static Texture2D rocketImg;
        public override Texture2D RocketImg { get => rocketImg; set => rocketImg = value; }
        public override string RocketName { get => rocketName; set => rocketName = value; }
        protected override void InitParameters()
        {
            base.InitParameters();

            Parameters[RocketProperties.Cooldown] = 5f;
            Parameters[RocketProperties.Acceleration] = 1f;

            Parameters[RocketProperties.Damage] = 5f;
            Parameters[RocketProperties.FuelCapacity] = 5f;
        }
    }
}
