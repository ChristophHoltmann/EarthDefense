using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour {

    public AudioSource source;
    public AudioClip takeOffSound;
    public AudioClip explosionSound;

    public RedFlickerEffect flicker;

	// Use this for initialization
	void Start () {

        Backend.SetEarth(this);
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collide");
        if (other.GetComponent<Meteor>() != null)
        {
            Backend.EarthHit();
            Destroy(other.gameObject);
            flicker.StartFlicker();

            source.clip = takeOffSound;
            source.Play();
            source.clip = explosionSound;
            source.Play();
        }
    }
}
