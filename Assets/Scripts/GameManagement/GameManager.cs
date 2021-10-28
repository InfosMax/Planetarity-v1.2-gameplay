using System.Collections.Generic;
using UnityEngine;
using Planetarity.Utility;
using Planetarity.RocketsFunctionality;
using Planetarity.UI;
using Planetarity.PlayerFunctionality;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Planetarity.GameManagement
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private int MinEnemyNumber = 3;
        [SerializeField]
        private int MaxEnemyNumber = 6;
        private bool isPaused = false;
        private ILevelGenerator levelGenerator;
        private UIController UI_Controller;
        public List<GameObject> AllPlanets { get; set; }
        public IRocketTypeManager RocketTypes { get; set; }
        public IRocketLaunchSystem RocketLauncher { get; set; }
        public RealPlayer MainPlayer { get; set; }
        public MaterialsStore PlanetMaterials { get; set; }
        public bool GameOver => AllPlanets.Count <= 1;

        void Awake()
        {
            RocketLauncher = new RocketLaunchSystem(this);
            RocketTypes = GetComponent<RocketTypeManager>();
            levelGenerator = GetComponent<LevelGenerator>();
            PlanetMaterials = GetComponent<MaterialsStore>();
            UI_Controller = GetComponent<UIController>();

            PrepareGameProcess();
        }

        void PrepareGameProcess()
        {
            AllPlanets = levelGenerator.Generate(MinEnemyNumber, MaxEnemyNumber);
            AssignPlayerScripts(AllPlanets);
            UI_Controller.Init(MainPlayer, AllPlanets);
        }

        private void AssignPlayerScripts(List<GameObject> Planets)
        {
            int playerIndex = Random.Range(0, Planets.Count);
            for (var i = 0; i < Planets.Count; i++)
            {
                if (playerIndex == i)
                {
                    MainPlayer = Planets[i].AddComponent<RealPlayer>();
                    MainPlayer.OnPlayerDied += PlayerDied;
                }
                else
                {
                    Planets[i].AddComponent<AIPlayer>().OnPlayerDied += PlayerDied;
                }

            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) && !UI_Controller.IsMainMenuActive)
                TogglePause();

            if (Input.GetKeyDown(KeyCode.Escape))
                ToggleMainMenu(!UI_Controller.IsMainMenuActive);
        }

        internal UnityAction SetPlayerRocketType()
        {
            throw new System.NotImplementedException();
        }

        public void TogglePause()
        {
            SetPause(!isPaused);
        }

        public void SetPause(bool doSet)
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
                SetPause(true);
                UI_Controller.ToggleMainMenu();
            }
            else
            {
                SetPause(false);
                UI_Controller.ToggleMainMenu();
            }
        }

        public void Restart()
        {
            SetPause(!isPaused);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void PlayerDied(Player deadPlayer)
        {
            AllPlanets.Remove(deadPlayer.gameObject);
            UI_Controller.ShowNotification(deadPlayer.DieInformText);
            
            if (MainPlayer && deadPlayer != MainPlayer && !MainPlayer.IsDead)
            {
                if (AllPlanets.Count == 1)
                    UI_Controller.ShowNotification($"Congratulations!\nYou have won the game!");
                else
                    UI_Controller.ShowNotification($"{AllPlanets.Count - 1 } enemies remaining! ");
            }

            deadPlayer.OnPlayerDied -= PlayerDied;
        }

        public void ShowNotification(string text) => UI_Controller.ShowNotification(text);

    }
}

