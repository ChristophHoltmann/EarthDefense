﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    private bool moving = false;
    private const float _speed = 0.75f;
   

    public int Value { get; private set; }
    public int Robustness { get; private set; }

    public void setParameters(int value, int robustness)
    {
        Value = value;
        Robustness = robustness;
    }

    public void OnClick()
    {
        Robustness--;
        if(Robustness <= 0)
        {
            Backend.DestroyMeteor(this);
        }
    }

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

        if (Input.GetKeyDown("c"))
        {
            print("clicked");
            OnClick();
        }
	}
}
