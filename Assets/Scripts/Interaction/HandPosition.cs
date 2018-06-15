using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class HandPosition : MonoBehaviour
{
    public GameObject foo;

    private 


    // Use this for initialization
    void Start()
    {
        InteractionManager.InteractionSourceDetected += Foo;
    }

    private void Foo(InteractionSourceDetectedEventArgs obj)
    {
        Vector3 position;
        if(obj.state.sourcePose.TryGetPosition(out position))
        {

        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
