using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour {

    public RedFlickerEffect flicker;

	// Use this for initialization
	void Start () {

        Backend.SetEarth(this);
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Backend.EarthHit();
        Destroy(other.gameObject);
        flicker.StartFlicker();
    }

   
}
