using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backend : MonoBehaviour {

    public int Score { get; private set; }
    public Earth Earth { get; private set; }
    public float RangeMaxMetoer { get; private set; }

    public int numberWaves { get; private set; }
    private int currentWave = -1;
    private int currentMeteorNumber = 0;
    private bool gameFinished = false;
    private float timer = 0;
    public float[] speedWaves { get; private set; }
    public int[] numberMeteorsPerWaves { get; private set; }

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
        numberWaves = 3;
        speedWaves = new float[]{ 2f, 1.5f, 1f };
        numberMeteorsPerWaves = new int[]{ 3, 3, 3 };


        // singelton
        _instance = this;
	}




	
	// Update is called once per frame
	void Update () {

        if (!gameFinished)
        {
            // check for end game 
            if (currentWave >= numberWaves)
            {
                Debug.Log("GAME WON");
                gameFinished = true;
            }
            if (Life <= 0)
            {
                Debug.Log("GAME OVER");
                gameFinished = true;
            }

            // start game 
            if (currentWave == -1)
            {
                Debug.Log("START GAME");
                currentWave = 0;
                Debug.Log("WAVE " + currentWave);
            }

            
            // check timer
            if (timer < 0f)
            {
                //reset timer based on wave speed
                timer = speedWaves[currentWave];

                // timer ready
                Debug.Log("WAVE: " + currentWave + " METEOR: " + currentMeteorNumber);
                AddMeteor();

                // increase meteornumber counter
                currentMeteorNumber++;

                // check for next wave
                if (currentMeteorNumber >= numberMeteorsPerWaves[currentWave])
                {
                    // next wave
                    currentWave++;
                    currentMeteorNumber = 0;
                    Debug.Log("WAVE " + currentWave);

                }

            }
            else
            {
                // decrease timer
                timer -= Time.deltaTime;
            }
        }

	}

    private void AddMeteor()
    {
        var newMeteor = Instantiate<GameObject>(prefabMeteor);
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
}
