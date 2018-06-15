using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlickerEffect : MonoBehaviour {

    float startTime;

    string emissionName = "_EmissionColor";

    Renderer renderer;
    Material mat;

    void Start()
    {
        startTime = Time.time;

        renderer = GetComponent<Renderer>();
        mat = renderer.material;
    }

    void Update () {
        if(Time.time - startTime <= 1f)
        {
            float emission = Mathf.PingPong(Time.time * 4, 1.0f);
            
            Color baseColor = Color.red;
            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

            mat.SetColor(emissionName, finalColor);
        } else
        {
            mat.SetColor(emissionName, Color.black);
        }
    }
}
