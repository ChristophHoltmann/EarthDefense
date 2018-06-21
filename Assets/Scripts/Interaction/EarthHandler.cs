using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHandler : MonoBehaviour, IInputClickHandler
{
    private bool isPlacing = true;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Backend.StartGame();

        isPlacing = false;
    }

    private void Update()
    {
        if (!isPlacing)
        {
            return;
        }

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;


        /* Calc. new display position (holdAtPosition). 
         * holdAtPosition is a point on the gaze direction vector (headposition -> gazeDirection)
         * whereas Distance gives the distance at which the display is held
         * see: linear function
         */
        Vector3 holdAtPosition = gazeDirection * 2 + headPosition; //+ offset

        //Update position
        this.transform.position = holdAtPosition;
    }
}
