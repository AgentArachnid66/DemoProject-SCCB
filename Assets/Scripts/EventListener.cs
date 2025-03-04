using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{


    private void OnEnable()
    {
        Debug.Log("Event Listener is Enabled");
        Singleton.Instance.UnityAction += UnityActionTest;
        Singleton.Instance.UnityAction_int += UnityAction_intTest;
    }

    private void UnityAction_intTest(int arg0)
    {
        Debug.Log($"Unity Action Triggered with value of {arg0}");
    }

    private void UnityActionTest()
    {
        Debug.Log("Unity Action Triggered");
    }

    public void UnityEvent_intTest(int arg0)
    {
        Debug.Log($"Unity Event Triggered with value of {arg0}");
    }

    public void UnityEventTest()
    {
        Debug.Log("Unity Event Triggered");
    }
}
