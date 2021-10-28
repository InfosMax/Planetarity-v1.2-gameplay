using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetarity.AstronomicalBodies;

namespace Planetarity.GameManagement
{
    public class LevelGenerator : MonoBehaviour, ILevelGenerator
    {
        [SerializeField]
        private GameObject sunPrefab;
        [SerializeField]
        private GameObject planetPrefab;

        private float distanceBetweenPlanets = 10f;
        public List<GameObject> Planets { get; set; }

        public List<GameObject> Generate(int enemiesMinNumber, int enemiesMaxNumber)
        {
            int enemiesNum = Random.Range(enemiesMinNumber, enemiesMaxNumber + 1);
            Debug.Log($"Enemies number is {enemiesNum}");

            return Generate(enemiesNum);
        }

        public List<GameObject> Generate(int enemiesNumber)
        {
            // Creating the Sun
            Instantiate(sunPrefab, Vector3.zero, Quaternion.identity);

            return CreatePlanets(enemiesNumber);
        }

        private List<GameObject> CreatePlanets(int planetsCount)
        {
            Planets = new List<GameObject>();

            for (var i = 0; i < planetsCount; i++)
            {
                Planets.Add( Instantiate(planetPrefab,
                    new Vector3(Sun.ComparingSize / 2f + (distanceBetweenPlanets * (i + 1)), 0f, 0f),
                    Quaternion.identity));
            }

            return Planets;
        }
    }
}
