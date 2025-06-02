using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBase : MammalBase, IFlight
{
    public bool bInFlight => InFlight;

    public void MaintainFlight()
    {
        throw new System.NotImplementedException();
    }

    public void StartFlight()
    {
        throw new System.NotImplementedException();
    }

    public void StopFlight()
    {
        throw new System.NotImplementedException();
    }

    protected bool InFlight;
}

