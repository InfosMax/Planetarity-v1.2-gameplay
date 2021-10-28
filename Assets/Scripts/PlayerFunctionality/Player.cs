using UnityEngine;
using Planetarity.GameManagement;
using Planetarity.RocketsFunctionality;
using Planetarity.AstronomicalBodies;
using System;

namespace Planetarity.PlayerFunctionality
{
    public abstract class Player : MonoBehaviour
    {
        protected const int DefaultRocketsNumber = 35;
        protected IRocketLaunchSystem rocketLaunchSystem;
        protected PlayerStats stats;
        protected RocketsStorage rocketsStorage;
        protected bool isOutOfROckets = false;
        protected GameManager gameManager;

        public delegate void StatChanged(float change);
        // Shows new HP.
        public event StatChanged HPchanged;
        // Shows new CD.
        public event StatChanged CooldownChanged;
        public event Action<Player> OnPlayerDied;
        public bool IsDead => stats.IsDead;
        public abstract string DieInformText { get; }

        //direction where to send the rocket
        protected abstract Vector3 GetRocketSendDirection();

        protected virtual void Awake()
        {
            stats = new PlayerStats() { 
                IsDead = false,
                HP = 1, 
                Cooldown = 5f
            };
            gameManager = GameManager.Instance;
            rocketLaunchSystem = gameManager.RocketLauncher;
            InitRocketStorage();
        }
        protected virtual void Start()
        {
            Invoke("InitGUI", 0.1f);
        }

        protected void InitGUI()
        {
            HPchanged?.Invoke(stats.HP);
            CooldownChanged?.Invoke(stats.Cooldown);
        }

        protected virtual void Update()
        {
            stats.Cooldown -= Time.deltaTime ;
        }

        protected void InitRocketStorage()
        {
            rocketsStorage = new RocketsStorage(DefaultRocketsNumber - 5, DefaultRocketsNumber + 5);
        }

        public RocketType[] GetAvailableRocketTypes()
        {
            return rocketsStorage.GetAvailableRocketTypes();
        } 

        protected virtual bool CheckLaunchPossible()
        {
            return stats.Cooldown <= 0f && !stats.IsDead && !gameManager.GameOver;
        }


        protected virtual void LaunchRocket(RocketType rocketType)
        {
            if(CheckLaunchPossible() )
            {
                float coolDown = rocketLaunchSystem.InitiateLaunch(this, rocketType, GetRocketSendDirection()).GetRocketCooldown();
                stats.Cooldown = coolDown;
                CooldownChanged?.Invoke(coolDown) ;
            }
        }

        public void GetDamage(float damage)
        {
            stats.HP -= damage;
            HPchanged?.Invoke(stats.HP >= 0f ? stats.HP : 0f);

            if (stats.HP <= 0f)
            {
                DestroyPlayer();
                OnPlayerDied?.Invoke(this);
            }
        }

        protected void DestroyPlayer()
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Planet>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            stats.IsDead = true;

            Destroy(gameObject, 4f);
        }
    }

    [System.Serializable]
    public struct PlayerStats
    {
        public bool IsDead;
        public float HP;
        public float Cooldown;
    }
}
