using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.Utility
{
    [System.Serializable]
    public class MaterialsStore : Singleton<MaterialsStore>
    {
        [SerializeField]
        private List<Material> materials;

        public List<Material> Materials { get => materials; set => materials = value; }

        public Material GetRandomPlanetMaterial()
        {
            return Materials[Random.Range(0, Materials.Count)];
        }
    }
}
