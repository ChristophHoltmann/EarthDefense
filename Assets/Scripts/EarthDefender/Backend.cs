using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backend : MonoBehaviour {

    public AudioSource source;
    public AudioClip bgMusic;
    public AudioClip gameOverSound;

    public int Score { get; private set; }
    public Earth Earth { get; private set; }
    public float RangeMaxMetoer { get; private set; }

    public TextMesh scoreText;
    public TextMesh messageText;
    public MeshRenderer[] hearts;
    private const int newWaveScore = 50;

    [SerializeField]
    private GameObject earthExplosion;

    //public int numberWaves { get; private set; }
    //private int currentWave = -1;
    private int currentMeteorNumber = 0;
    private bool gameFinished = false;
    private float timer = 0;
    private float messageTimer = 0;
    private const float messageTime = 2.5f;
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
    public GameObject particlePrefab;

    public static Vector3 getEarthPos()
    {
        return _instance.Earth.gameObject.transform.position;
    }


    // Use this for initialization
    void Awake () {
        //source.Play((AudioClip)Resources.Load("bgMusic"));
        
        // Init values
        Score = 0;
        Life = 3;
        RangeMaxMetoer = 3f;
        var newWaveTime = messageTime;
        // time, value, robustness
        // robustness new wave
        meteors = new float[][] {
            new float[]{ newWaveTime, 0f },
            new float[]{2f,1f }, new float[] { 2f, 1f }, new float[] { 2f, 1f },
            new float[]{ newWaveTime, 0f },
            new float[]{1.5f,1f }, new float[]{ 1.5f, 1f },new float[]{ 1.5f, 2f },
            new float[]{ newWaveTime, 0f },
            new float[]{1f,1f },new float[]{1f,1f },new float[]{1f,2f },new float[]{1f,2f },
            new float[]{ newWaveTime, 0f },
            new float[]{0.5f,3f }, new float[]{ 0.5f,3f },new float[]{ 0.5f,3f },};


        // singelton
        _instance = this;

        source.clip = bgMusic;
        source.Play();
    }





    // Update is called once per frame
    void Update () {

        ShowScore();
        ShowHearts();

        if (!gameFinished)
        {
            // check for end game 
            if (currentMeteorNumber >= meteors.Length)
            {
                ShowMessage("GAME WON");
                gameFinished = true;
            }
            if (Life <= 0)
            {
                ShowMessage("GAME OVER");

                if(earthExplosion != null)
                {
                    var exlopsionObject = Instantiate(earthExplosion);
                    exlopsionObject.transform.position = Earth.transform.position;

                    Earth.transform.Find("EarthHigh").gameObject.SetActive(false);
                }

                gameFinished = true;
                source.Stop();
                source.clip = null;
                //source.PlayOneShot((AudioClip)Resources.Load("gameOver"));
                source.clip = gameOverSound;
                source.Play();
            }

                        
            // check timer
            if (timer < 0f)
            {
                var values = meteors[currentMeteorNumber];

                if(values[1] < 0.001f)
                {
                    //new wave
                    ShowMessage("NEW WAVE");
                    Score += newWaveScore;
                }
                else
                {
                    // timer ready
                    AddMeteor((int)calcScore(values[0], values[1]),(int)values[1]);
                }               

                //reset timer based on wave speed
                timer = values[0];

                currentMeteorNumber++;

            }
            else
            {
                // decrease timer
                timer -= Time.deltaTime;
            }

            if (messageTimer < 0f)
            {
                HideMessage();
            }
            else
            {
                messageTimer -= Time.deltaTime;
            }
        }

	}

    public List<GameObject> asteroids;

    GameObject CreateRandomPrefab()
    {
        int idx = UnityEngine.Random.Range(0, asteroids.Count);

        var go =  Instantiate<GameObject>(asteroids[idx]);

        //rescale go
        var scale = 0.1f;
        go.transform.localScale = new Vector3(scale, scale, scale);
        go.AddComponent<Meteor>();
        go.AddComponent<SphereCollider>();
        var col = go.GetComponent<SphereCollider>();
        col.isTrigger = true;

        Instantiate(particlePrefab, go.transform);

        return go;
    }

    private float calcScore(float timing, float robustness)
    {
        return 10f * robustness * ((2f - timing) + 1f);
    }

    private void ShowHearts()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(Life > i)
            {
                //red
                hearts[i].material.color = Color.red;
            }
            else
            {
                //grey 
                hearts[i].material.color = new Color(135, 135, 135);
            }
        }
    }

    private void AddMeteor(int value, int robustness)
    {
        var newMeteor = CreateRandomPrefab();

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
    }

    private void ShowScore()
    {
        scoreText.text = "SCORE:\n" + Score;
    }

    private void HideMessage()
    {
        messageText.text = "";
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;
        messageTimer = messageTime;
    }
}
