using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.GameManagement;
using Planetarity.RocketsFunctionality;

namespace Planetarity.PlayerFunctionality
{
    public class RealPlayer: Player
    {
        private string selectedRocketType;
        public string SelectedRocketType { get => selectedRocketType; set => selectedRocketType = value; }

        protected override bool CheckLaunchPossible(string rocketName)
        {
            return rocketsStorage.TryGetRocket(selectedRocketType) && base.CheckLaunchPossible(rocketName) ;
        }

        protected override Vector3 GetRocketDirrection()
        {
            // Temporary
            return Vector3.up;
        }

        public void SetRocketType(string rocketType)
        {
            selectedRocketType = rocketType;
        }

    }
}
