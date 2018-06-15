using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    private bool moving = false;
    private const float _speed = 0.75f;
   

	void moveMeteor()
    {
       
        Vector3 posEarth = Backend.getEarthPos();
        Vector3 direction = posEarth - transform.position;
        direction.Normalize();
        transform.position = transform.position + (Time.deltaTime * _speed * direction);
    }
	
	// Update is called once per frame
	void Update () {

        moveMeteor();
	}
}
