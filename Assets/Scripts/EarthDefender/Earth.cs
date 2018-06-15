using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Backend.SetEarth(this);
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collide");
        Backend.EarthHit();
        Destroy(other.gameObject);
    }

   
}
