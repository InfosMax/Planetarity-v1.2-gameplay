using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRocketFollow : MonoBehaviour
{
    public Transform followedTransf;
    private Transform thisTr;

    private void Start()
    {
        thisTr = transform;
    }
    void Update()
    {
        if(followedTransf != null)
            thisTr.position = followedTransf.position + new Vector3(0f, 7f, -15f);
    }
}
