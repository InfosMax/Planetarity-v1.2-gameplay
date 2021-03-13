using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.GameManagement;

namespace Planetarity.PlayerFunctionality
{
    public class Player : MonoBehaviour
    {
        private GameManager gameManager;

        void Start()
        {
            gameManager = GameManager.Instance;
        }
    }
}
