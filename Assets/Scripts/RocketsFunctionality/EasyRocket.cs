using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.GameManagement;

namespace Planetarity.RocketsFunctionality
{

    // Easy-to-build rocket:
    // minimal cooldown, a bit lower damage, other - standard.
    public class EasyRocket : Rocket
    {
        protected static GameObject rocketPrefab;
        public override GameObject RocketPrefab { get => rocketPrefab; set => rocketPrefab = value; }
        protected static string rocketName = "EasyRocketPref";
        public override string RocketName { get => rocketName; set => rocketName = value; }

        protected override void InitParameters()
        {
            base.InitParameters();

            Parameters[RocketProperties.Cooldown] = 1;

            Parameters[RocketProperties.Damage] = 2;
        }
    }
}
