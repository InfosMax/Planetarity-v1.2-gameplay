using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.GameManagement;

namespace Planetarity.PlayerFunctionality
{
    public class RealPlayer: Player
    {
        private void Start()
        {
            gameManager = GameManager.Instance;
            GameObject rocket = Instantiate(gameManager.GetRocketResources("HeavyRocket").Item1, transform.position, Quaternion.identity);
        }


    }
}
