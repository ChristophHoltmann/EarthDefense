using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlickerEffect : MonoBehaviour {

    float startTime;

    string emissionName = "_EmissionColor";

    public float flickerLength = 1f;

    public Renderer renderer;
    Material mat;

    void Start()
    {
        mat = renderer.material;
    }

    void Update () {
        if(Time.time - startTime <= flickerLength)
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

    internal void StartFlicker()
    {
        startTime = Time.time;
    }
}
