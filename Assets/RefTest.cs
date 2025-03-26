using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class myObject
{
    public int someInt;
}


public class RefTest : MonoBehaviour
{

    myObject ChangeObject(ref myObject A)
    {
        A.someInt = 10;
        A = new myObject();
        A.someInt = 20;

        return A;
    }

    [ContextMenu("Test Ref")]
    void Test()
    {
        myObject a = new myObject();
        a.someInt = 5;
        myObject b = ChangeObject(ref a);
        Debug.Log(a.someInt);
        Debug.Log(b.someInt);
    }
}
