using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.Utility;
using Planetarity.RocketsFunctionality;
using Planetarity.UI;
using Planetarity.PlayerFunctionality;
using UnityEngine.SceneManagement;

namespace Planetarity.GameManagement
{
    public class GameManager : Singleton<GameManager>, IRocketPrefabManager
    {
        private bool isPaused = false;
        private RocketLaunchStation rocketLaunchStation;
        private LevelGenerator levelGenerator;
        private UIController UI_Controller;
        private RocketPrefabsManager rocketPrefabManager;
        private MaterialsStore materialsStore;
        private RealPlayer player;
        public RealPlayer Player { get => player; set => player = value; }
        public MaterialsStore PlanetMaterials { get => materialsStore; set => materialsStore = value; }

        void Awake()
        {
            rocketPrefabManager = new RocketPrefabsManager();

            rocketLaunchStation = GetComponent<RocketLaunchStation>();
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
                    Player = Planets[i].AddComponent<RealPlayer>();
                    //Camera.main.transform.parent = Planets[i].transform;
                }
                else
                {
                    Planets[i].AddComponent<AIPlayer>();
                }
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) && !UI_Controller.IsMainMenuActive)
            {
                togglePause();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleMainMenu(!UI_Controller.IsMainMenuActive);
            }

        }

        public void togglePause()
        {
            setPause(!isPaused);
        }

        public void setPause(bool doSet)
        {
            if (doSet)
            {
                isPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1f;
            }
                
        }

        public void ToggleMainMenu(bool activate)
        {
            if (activate)
            {
                setPause(true);
                UI_Controller.ToggleMainMenu();
            }
            else
            {
                setPause(false);
                UI_Controller.ToggleMainMenu();
            }
        }

        public void Restart()
        {
            setPause(!isPaused);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void SelectPlayerRocketName(string rocketName)
        {
            player.SetRocketType(rocketName);
        }
        public void PlayerDied(Player player) => levelGenerator.removePlayerPlanetFromList(player.gameObject);

        public GameObject[] GetPlanets() => levelGenerator.getPlanetsGO();

        public RocketLaunchStation GetRocketLaunchStation() => rocketLaunchStation;

        public void AddRocket(KeyValuePair<string, (GameObject, Texture2D)> rocket) => RocketPrefabsManager.AddRocket(rocket);

        public (GameObject, Texture2D) GetRocketResources(string rocketName) => rocketPrefabManager.GetRocketResources(rocketName);

        public GameObject GetRocketPrefab(string rocketName) => rocketPrefabManager.GetRocketPrefab(rocketName);

        public string[] GetRocketsNames() => rocketPrefabManager.GetRocketsNames();

        public void ShowNotification(string text) => UI_Controller.ShowNotification(text);

    }
}

