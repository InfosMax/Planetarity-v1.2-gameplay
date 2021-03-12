using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialsStore : MonoBehaviour
{
    [SerializeField]
    private  List<Material> materials;

    public List<Material> Materials { get => materials; set => materials = value; }

    public Material GetRandomPlanetMaterial()
    {
        return Materials[Random.Range(0, Materials.Count)];
    }
}
