using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BasicInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovement;

    // Update is called once per frame
    void Update()
    {
        Vector2 CompiledMovement = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Moving Forward");
            CompiledMovement.y = 1;
        }


        OnMovement.Invoke(CompiledMovement);
    }
}
