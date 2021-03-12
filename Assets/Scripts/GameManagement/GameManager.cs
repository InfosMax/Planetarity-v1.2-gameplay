using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Color camBackgroundColor1;
    public Color camBackgroundColor2;
    private MaterialsStore materialsStore;
    private LevelGenerator levelGenerator;

    public MaterialsStore PlanetMaterials { get => materialsStore; set => materialsStore = value; }

    void Start()
    {
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

}
