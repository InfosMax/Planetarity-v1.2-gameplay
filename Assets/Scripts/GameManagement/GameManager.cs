using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.Utility;
using Planetarity.RocketsFunctionality;

namespace Planetarity.GameManagement
{
    public class GameManager : Singleton<GameManager>, IRocketPrefabManager
    {
        public Color camBackgroundColor1;
        public Color camBackgroundColor2;
        private LevelGenerator levelGenerator;
        private RocketPrefabsManager rocketPrefabManager;

        private MaterialsStore materialsStore;
        public MaterialsStore PlanetMaterials { get => materialsStore; set => materialsStore = value; }

        void Start()
        {
            rocketPrefabManager = new RocketPrefabsManager();
            levelGenerator = GetComponent<LevelGenerator>();
            PlanetMaterials = GetComponent<MaterialsStore>();

            prepareGameProcess();
        }

        void prepareGameProcess()
        {
            levelGenerator.Generate(3, 6);
        }

        void CameraBackgroundColorLerp()
        {
            Camera.main.backgroundColor = Color.Lerp(camBackgroundColor1, camBackgroundColor2, Mathf.PingPong(Time.unscaledTime * 0.1f, 1));
        }

        private void Update()
        {
            CameraBackgroundColorLerp();
        }

        public void AddRocket(KeyValuePair<string, GameObject> rocket)
        {
            rocketPrefabManager.AddRocket(rocket);
        }

        public GameObject GetRocketPrefab(string rocketName)
        {
            return rocketPrefabManager.GetRocketPrefab(rocketName);
        }
    }
}

