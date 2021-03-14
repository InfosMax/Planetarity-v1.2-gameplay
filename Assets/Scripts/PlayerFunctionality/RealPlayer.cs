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

        protected override void Start()
        {
            base.Start();
            selectedRocketType = gameManager.GetRocketsNames()[0];
        }

        protected override Vector3 GetRocketDirrection()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 mouseDirrection = (mousePos - transform.position).normalized;

            Debug.DrawLine(transform.position, transform.position + mouseDirrection * 10, Color.red, Mathf.Infinity);

            return mouseDirrection;
        }

        public void SetRocketType(string rocketType)
        {
            selectedRocketType = rocketType;
            Debug.Log($"Rocket type set to {rocketType}");
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(0))
            {
                LaunchRocket(selectedRocketType);
            }
        }

    }
}
