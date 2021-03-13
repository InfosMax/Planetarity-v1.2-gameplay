using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.GameManagement;

namespace Planetarity.PlayerFunctionality
{
    public class Player : MonoBehaviour
    {
        protected GameManager gameManager;
        protected PlayerStats stats;

        public void GetDamage(float damage)
        {
            if (stats.HP - damage <= 0f)
            {
                DestroyPlayer();
            }   
            else
            {
                stats.HP -= damage;
            }
        }

        public void launchRocket()
        {

        }

        protected void DestroyPlayer()
        {
            Destroy(gameObject);
        }
    }

    public class PlayerStats
    {
        public float HP { get; set; } = 50;
        public float Cooldown { get; set; } = 5f;
    }
}
