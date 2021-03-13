using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.Utility;
using Planetarity.RocketsFunctionality;
using Planetarity.UI;
using Planetarity.PlayerFunctionality;

namespace Planetarity.GameManagement
{
    public class GameManager : Singleton<GameManager>, IRocketPrefabManager
    {
        private LevelGenerator levelGenerator;
        private UIController UI_Controller;
        private RocketPrefabsManager rocketPrefabManager;
        private MaterialsStore materialsStore;
        private RealPlayer player;
        public MaterialsStore PlanetMaterials { get => materialsStore; set => materialsStore = value; }

        void Awake()
        {
            rocketPrefabManager = new RocketPrefabsManager();
            levelGenerator = GetComponent<LevelGenerator>();
            PlanetMaterials = GetComponent<MaterialsStore>();
            UI_Controller = GetComponent<UIController>();

            prepareGameProcess();
        }

        void prepareGameProcess()
        {
            levelGenerator.Generate(3, 6);
            AssignPlayerScripts(levelGenerator.getPlanetsGO());
        }

        private void AssignPlayerScripts(GameObject[] Planets)
        {
            int playerIndex = Random.Range(0, Planets.Length);
            for (var i = 0; i < Planets.Length; i++)
            {
                if(playerIndex == i)
                {
                    player = Planets[i].AddComponent<RealPlayer>();
                    //Camera.main.transform.parent = Planets[i].transform;
                }
                else
                {
                    Planets[i].AddComponent<AIPlayer>();
                }
            }
        }

        public void AddRocket(KeyValuePair<string, (GameObject, Texture2D)> rocket)
        {
            rocketPrefabManager.AddRocket(rocket);
        }

        public (GameObject, Texture2D) GetRocketResources(string rocketName)
        {
            return rocketPrefabManager.GetRocketResources(rocketName);
        }

    }
}

