using Planetarity.RocketsFunctionality;
using System.Collections;
using UnityEngine;

namespace Planetarity.PlayerFunctionality
{
    public class AIPlayer : Player
    {
        protected float botThinkDelay = 2f;

        public override string DieInformText { get; } = "Bot is destroyed!";

        protected override void Start()
        {
            base.Start();
            StartCoroutine(TryLaunch());
        }

        //send rocket to random enemy
        protected override Vector3 GetRocketSendDirection()
        {
            var enemies = gameManager.AllPlanets;
            // remove this player planet from enemies
            if (enemies.Count > 1)
            {
                GameObject randomPlanet;
                do
                    randomPlanet = enemies[Random.Range(0, enemies.Count)];
                while (randomPlanet == gameObject);

                Vector3 enemyDir = (randomPlanet.transform.position - transform.position).normalized;
                return enemyDir;
            }
            return Vector3.zero;
        }

        private IEnumerator TryLaunch()
        {
            yield return new WaitForSeconds(botThinkDelay);
            RocketType? rocketType;
            do
            {
                rocketType = rocketsStorage.GetAnyRocket();

                if (rocketType != null)
                {
                    LaunchRocket(rocketType.Value);
                    yield return new WaitWhile(() => stats.Cooldown > 0f);
                    yield return new WaitForSeconds(Random.Range(0f, botThinkDelay));
                }  
            } while (rocketType != null);
            Debug.Log($"Player {gameObject.name} is out of rockets!");
        }
    }
}

