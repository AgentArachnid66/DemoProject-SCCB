using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MammalBase : AnimalBase
{
    protected override void OnMove()
    {
        base.OnMove();
        Debug.Log("MammalBase On Move");
    }

    private void Start()
    {
        OnMove();
    }

}

