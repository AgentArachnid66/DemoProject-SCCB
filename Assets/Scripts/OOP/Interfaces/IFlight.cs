using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IFlight
{
    public void StartFlight();

    public void MaintainFlight();

    public void StopFlight();

    public bool bInFlight { get; }

}
