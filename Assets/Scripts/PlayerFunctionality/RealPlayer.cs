using UnityEngine;
using Planetarity.RocketsFunctionality;
using UnityEngine.EventSystems;

namespace Planetarity.PlayerFunctionality
{
    public class RealPlayer: Player
    {
        private RocketType selectedRocketType;
        private EventSystem eventSystem;

        public override string DieInformText { get; } = "YOU HAVE LOST THE GAME!!";

        protected override bool CheckLaunchPossible()
        {
            if (base.CheckLaunchPossible())
                if (rocketsStorage.TryGetRocket(selectedRocketType))
                    return true;
                else
                {
                    gameManager.ShowNotification("You don't have such a rocket!");
                    return false;
                }
            else
            {
                gameManager.ShowNotification("Launch not ready yet!");
                return false;
            }
        }

        protected override void Start()
        {
            base.Start();
            selectedRocketType = (RocketType)0;
            eventSystem = EventSystem.current;
        }

        protected override Vector3 GetRocketSendDirection()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            return (mousePos - transform.position).normalized;
        }

        public void SetRocketType(RocketType rocketType)
        {
            selectedRocketType = rocketType;
            gameManager.ShowNotification($"Rocket type set to {rocketType}");
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(0) && !eventSystem.currentSelectedGameObject)
            {
                LaunchRocket(selectedRocketType);
            }
        }

    }
}
