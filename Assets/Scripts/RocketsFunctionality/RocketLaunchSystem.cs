using UnityEngine;
using Planetarity.GameManagement;
using Planetarity.PlayerFunctionality;

namespace Planetarity.RocketsFunctionality
{
    public class RocketLaunchSystem : IRocketLaunchSystem
    {
        private GameManager GM;

        public RocketLaunchSystem(GameManager gameManager)
        {
            GM = gameManager;
        }

        public Rocket InitiateLaunch(Player initiator, RocketType rocketName, Vector3 direction)
        {
            RocketParameters parameters = GM.RocketTypes.GetRocketParameters(rocketName);
            Rocket newRocket = GameObject.Instantiate(parameters.prefab, 
                initiator.transform.position + direction * initiator.transform.localScale.x,
                Quaternion.LookRotation(direction, Vector3.back) ).AddComponent<Rocket>();

            newRocket.OverwriteProperties(parameters);
            newRocket.LauncherPlanet = initiator.gameObject;

            return newRocket;
        }
    }
}

