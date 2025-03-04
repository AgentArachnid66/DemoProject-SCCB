using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Singleton : MonoBehaviour
{
    private static Singleton _instance;
    public static Singleton Instance { get { return _instance; } }


    public UnityEvent UnityEvent;
    public UnityAction UnityAction;// = new UnityAction();

    public UnityEvent<int> UnityEvent_int;
    public UnityAction<int> UnityAction_int;

    

    private void Awake()
    {
        Debug.Log("Singleton Awaking");
        if (_instance != null && _instance != this)
        {
            Debug.LogError($"Multiple Singleton Instances, destroying {this}");
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    [ContextMenu("Test Trigger Events")]
    public void TestTriggerEvents()
    {
        UnityEvent.Invoke();
        UnityEvent_int.Invoke(5);
    }

    [ContextMenu("Test Trigger Actions")]
    public void TestTriggerActions()
    {
        UnityAction.Invoke();
        UnityAction_int.Invoke(5);
    }
}


