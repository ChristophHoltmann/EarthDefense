using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefab : MonoBehaviour {
    public List<GameObject> asteroids;

    void Start () {
        // Y U NO WORK?
        //asteroids = new List<GameObject>(Resources.LoadAll<GameObject>("/Asteroids/Prefabs/Asteroids/"));
        //asteroids.AddRange(Resources.LoadAll<GameObject>("/Asteroids/Prefabs/AsteroidsElectric/"));
        //asteroids.AddRange(new List<GameObject>(Resources.LoadAll<GameObject>("/Asteroids/Prefabs/AsteroidsRocky/")));
        //asteroids.AddRange(new List<GameObject>(Resources.LoadAll<GameObject>("/Asteroids/Prefabs/AsteroidsLava/")));
        CreateRandomPrefab();
    }

    void CreateRandomPrefab()
    {
        int idx = Random.Range(0, asteroids.Count);

        Instantiate(asteroids[idx]);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
