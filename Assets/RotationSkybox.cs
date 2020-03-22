using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSkybox : MonoBehaviour {

    public float SkyboxSpeed = 1f;


    void Update ()
    {

        RenderSettings.skybox.SetFloat("_Rotation", Time.time* SkyboxSpeed);

    }

}
