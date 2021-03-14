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

        public delegate void statChanged(float change);
        // Shows new HP.
        public event statChanged HPchanged;
        // Shows new CD.
        public event statChanged CooldownChanged;

        protected virtual void Start()
        {
            stats = new PlayerStats();
            gameManager = GameManager.Instance;
            rocketLauncStation = gameManager.GetRocketLaunchStation();
            InitRocketStorage();
            InitGUI();
        }

        protected void InitGUI()
        {
            if (HPchanged != null)
                HPchanged(stats.HP);

            if (CooldownChanged != null)
                CooldownChanged(stats.Cooldown);
        }

        protected virtual void Update()
        {
            stats.Cooldown -= Time.deltaTime;
        }

        protected void InitRocketStorage()
        {
            rocketsStorage = new RocketsStorage();
            rocketsStorage.Init(gameManager.GetRocketsNames(), 30, 40); 
        }

        public void GetDamage(float damage)
        {
            if (stats.HP - damage <= 0f)
            {
                stats.HP -= damage;
                DestroyPlayer();
            }   
            else
            {
                stats.HP -= damage;
            }
            if (HPchanged != null)
            {
                HPchanged(stats.HP >= 0f ? stats.HP : 0f);
            }
                
        }

        protected virtual bool CheckLaunchPossible(string rocketName)
        {
            if (stats.Cooldown <= 0f && !stats.isDead)
            {
                return true;
            }
            else
            {
                if(this is RealPlayer)
                    gameManager.ShowNotification("Launch not ready yet!");
                return false;
            }
               
        }


        protected void LaunchRocket(string rocketName)
        {
            if(CheckLaunchPossible(rocketName) )
            {
                float coolDown = rocketLauncStation.InitiateLaunch(this, rocketName, GetRocketDirrection()).GetRocketCooldown();
                stats.Cooldown = coolDown;
                if(CooldownChanged != null)
                {
                    CooldownChanged(coolDown);
                }
            }
        }

        protected abstract Vector3 GetRocketDirrection();

        protected void DestroyPlayer()
        {
            gameManager.PlayerDied(this);
            if (this is RealPlayer)
            {
                gameManager.ShowNotification("YOU HAVE LOST THE GAME!!");
            }
            else
            {
                gameManager.ShowNotification($"Bot is destroyed!\n{gameManager.GetPlanets().Length - 1 } is remaining! ");
                if(gameManager.GetPlanets().Length == 1)
                    gameManager.ShowNotification($"Congratulations!\nYou have won the game!");
            }


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
