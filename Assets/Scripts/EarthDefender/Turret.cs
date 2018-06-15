using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private float shootingTimer = 0f;
    private const float ShootingTime = 2f;

    public ParticleSystem laserBeam;

    //private void OnTriggerEnter(Collider other)
    //{
    //    enteredMeteors.Add(other.gameObject.GetComponent<Meteor>());
    //    Debug.Log("ENTERED");
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    var exitMeteor = other.gameObject.GetComponent<Meteor>();
    //    enteredMeteors.Remove(exitMeteor);
    //}

    private void OnTriggerStay(Collider other)
    {
        var enteredMeteor = other.gameObject.GetComponent<Meteor>();

        if (enteredMeteor != null)
        {

            if (shootingTimer <= 0f)
            {
                print("shoot");
                //shoot
                Shoot(enteredMeteor);

                //reset timer
                shootingTimer = ShootingTime;
            }
        }
    }

   

    // Update is called once per frame
    void Update () {
        if (shootingTimer > 0f)
        {
            shootingTimer -= Time.deltaTime;
        }
		
	}

    private void Shoot(Meteor meteor)
    {
        //look at meteor
        var rotation = Quaternion.LookRotation(meteor.gameObject.transform.position - transform.position);
        transform.rotation = rotation;

        // show beam
        if (laserBeam.isPlaying) laserBeam.Stop();
        if (!laserBeam.isPlaying)laserBeam.Play();

        //click meteor

        meteor.OnClick();
    }
}
