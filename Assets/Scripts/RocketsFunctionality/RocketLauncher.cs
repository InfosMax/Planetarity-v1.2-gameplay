using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    public class RocketLauncher : MonoBehaviour, IRocketLauncher
    {
        private List<Rocket> rocket = new List<Rocket>();
        public List<Rocket> Rocket { get => rocket; set => rocket = value; }

        public void InitiateLaunch(Rocket rocket)
        {
            throw new System.NotImplementedException();
        }
    }
}

