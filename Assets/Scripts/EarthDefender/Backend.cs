﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backend : MonoBehaviour {

    public int Score { get; private set; }
    public Earth Earth { get; private set; }
    public float RangeMaxMetoer { get; private set; }

    //public int numberWaves { get; private set; }
    //private int currentWave = -1;
    private int currentMeteorNumber = 0;
    private bool gameFinished = false;
    private float timer = 0;
    public float[][] meteors { get; private set; }
    //public int[] numberMeteorsPerWaves { get; private set; }

    public static void SetEarth(Earth earth)
    {
        _instance.Earth = earth;
    }

    public static void EarthHit()
    {
        _instance.Life--;
        Debug.Log(_instance.Life);
    }

    public int Life { get; private set; }
    private static Backend _instance;
    public GameObject prefabMeteor;

    public static Vector3 getEarthPos()
    {
        return _instance.Earth.gameObject.transform.position;
    }


    // Use this for initialization
    void Awake () {
        // Init values
        Score = 0;
        Life = 30;
        RangeMaxMetoer = 3f;
        // speed, value, robustness
        // robustness new wave
        meteors = new float[][] {
            new float[]{2f,0f,0f },
            new float[]{2f,10f,1f }, new float[] { 2f, 10f, 1f }, new float[] { 2f, 10f, 1f },
            new float[]{2f,50f,0f },
            new float[]{1.5f,10f,1f }, new float[]{ 1.5f, 10f,1f },new float[]{ 1.5f, 30f,2f },
            new float[]{2f,50f,0f },
            new float[]{1f,15f,1f },new float[]{1f,15f,1f },new float[]{1f,30f,2f },new float[]{1f,30f,2f },
            new float[]{2f,50f,0f },
            new float[]{0.5f,30f,3f }, new float[]{ 0.5f,30f,3f },new float[]{ 0.5f,30f,3f },};


        // singelton
        _instance = this;
	}




	
	// Update is called once per frame
	void Update () {

        if (!gameFinished)
        {
            // check for end game 
            if (currentMeteorNumber >= meteors.Length)
            {
                Debug.Log("GAME WON");
                gameFinished = true;
            }
            if (Life <= 0)
            {
                Debug.Log("GAME OVER");
                gameFinished = true;
            }

                        
            // check timer
            if (timer < 0f)
            {
                var values = meteors[currentMeteorNumber];

                if(values[2] < 0.001f)
                {
                    //new wave
                    Debug.Log("NEW WAVE");
                }

                // timer ready
                AddMeteor((int) values[1],(int) values[2]);

                //reset timer based on wave speed
                timer = values[0];

                currentMeteorNumber++;

            }
            else
            {
                // decrease timer
                timer -= Time.deltaTime;
            }
        }

	}

    private void AddMeteor(int value, int robustness)
    {
        var newMeteor = Instantiate<GameObject>(prefabMeteor);

        var meteor = newMeteor.GetComponent<Meteor>();
        meteor.setParameters(value, robustness);

        // Debug.Log("add meteor");
        var earthPos = getEarthPos();
        // default position
        var meteorPosRelative = new Vector3(RangeMaxMetoer, 0, 0);

        // rotate pos random
        var earthAround = UnityEngine.Random.Range(0f, 1f);
        var angleUp = UnityEngine.Random.Range(0f, 1f);
        meteorPosRelative = Quaternion.Euler(0, 360 * earthAround, 45 * angleUp) * meteorPosRelative;
        //Debug.Log(meteorPosRelative);

        //set position
        newMeteor.transform.position = earthPos + meteorPosRelative;
    }

    public static void DestroyMeteor(Meteor meteor)
    {
        _instance.Score += meteor.Value;
        Destroy(meteor.gameObject);

        Debug.Log("SCORE: " + _instance.Score);
    }
}
