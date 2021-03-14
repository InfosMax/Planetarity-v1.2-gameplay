using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.GameManagement;
using Planetarity.RocketsFunctionality;
using Planetarity.AstronomicalBodies;

namespace Planetarity.PlayerFunctionality
{
    public abstract class Player : MonoBehaviour
    {
        protected RocketLaunchStation rocketLauncStation;
        protected GameManager gameManager;
        protected PlayerStats stats;
        protected RocketsStorage rocketsStorage;

        protected virtual void Start()
        {
            stats = new PlayerStats();
            gameManager = GameManager.Instance;
            rocketLauncStation = gameManager.GetRocketLaunchStation();
            InitRocketStorage();
        }

        protected virtual void Update()
        {
            stats.Cooldown -= Time.deltaTime;
        }

        protected void InitRocketStorage()
        {
            rocketsStorage = new RocketsStorage();
            rocketsStorage.Init(gameManager.GetRocketsNames(), 15, 35); 
        }

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

        protected virtual bool CheckLaunchPossible(string rocketName) => stats.Cooldown <= 0f;

        protected void LaunchRocket(string rocketName)
        {
            if(CheckLaunchPossible(rocketName) )
            {
                float coolDown = rocketLauncStation.InitiateLaunch(this, rocketName, GetRocketDirrection()).GetRocketCooldown();
                stats.Cooldown = coolDown;
            }
        }

        protected abstract Vector3 GetRocketDirrection();

        protected void DestroyPlayer()
        {
            if (this.GetType() == typeof(RealPlayer))
                Debug.Log("YOU HAVE LOST THE GAME!!");

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Planet>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            stats.isDead = true;

            Destroy(gameObject, 4f);
        }
    }

    [System.Serializable]
    public class PlayerStats
    {
        public bool isDead = false;
        public float HP { get; set; } = 50;
        public float Cooldown { get; set; } = 5f;
    }
}
