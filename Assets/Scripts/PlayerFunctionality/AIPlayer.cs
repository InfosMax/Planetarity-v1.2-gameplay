using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.PlayerFunctionality
{
    public class AIPlayer : Player
    {
        protected override Vector3 GetRocketDirrection()
        {
            // Vector3 playerDir = (gameManager.Player.transform.position - transform.position).normalized;

            // Better to calculate closest enemy
            GameObject[] enemies = gameManager.GetPlanets();

            Vector3 enemyDir = (enemies[Random.Range(0, enemies.Length)].transform.position - transform.position).normalized;
            Vector3 deviation = new Vector3(Random.value /3f , Random.value / 3f, 0f);
            return enemyDir + deviation;
        }

        protected override void Start()
        {
            base.Start();

            StartCoroutine(TryLaunch());
        }

        private IEnumerator TryLaunch()
        {
            yield return new WaitForSeconds(2f);
            string rocketName;
            do
            {
                rocketName = rocketsStorage.GetAnyRocket();
                Debug.Log($"AI bot rocketName is {rocketName}");
                if (rocketName != null)
                {
                    LaunchRocket(rocketName);
                    yield return new WaitWhile(() => stats.Cooldown > 0f);
                    yield return new WaitForSeconds(2f);
                }
                else
                    Debug.Log($"Player {gameObject.name} is out of rockets!");
            } while (rocketName != null);

        }
    }
}

