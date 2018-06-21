using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float rotationSpeed = 1.0f;
	
	void Update () {
        transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
    }
}
