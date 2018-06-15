using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour, ISourceStateHandler
{
    public TextMesh debugText;

    private void Start()
    {
        InputManager.Instance.AddGlobalListener(gameObject);
    }

    public void OnSourceDetected(SourceStateEventData eventData)
    {
        debugText.text += "DETECTED";
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        debugText.text += "LOST";
    }
}
