using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.Utility
{
    [System.Serializable]
    public class MaterialsStore : MonoBehaviour
    {
        [SerializeField]
        private List<Material> materials;

        public Material GetRandomPlanetMaterial()
        {
            return materials[Random.Range(0, materials.Count)];
        }
    }
}
