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
        protected static string rocketName = "ShortRocket";
        public override string RocketName { get => rocketName; set => rocketName = value; }
        protected static Texture2D rocketImg;
        public override Texture2D RocketImg { get => rocketImg; set => rocketImg = value; }

        protected override void InitParameters()
        {
            base.InitParameters();

            Parameters[RocketProperties.FuelCapacity] = 1f;

            Parameters[RocketProperties.Damage] = 4f;
            Parameters[RocketProperties.Cooldown] = 2f;
        }
    }
}


