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

        Debug.Log("Post Change Object");
        return A;
    }

    void In_Test(in myObject A)
    {
        Debug.Log(A.someInt);
        A.someInt = 1;
        // A = new myObject();
        Debug.Log("Post In Test");
    }

    void Out_Test(out myObject B)
    {
        B = new myObject();
        B.someInt = 10;
        Debug.Log("Post Out Test");
    }

    [ContextMenu("Test Ref")]
    void Test()
    {
        myObject A;
        Out_Test(out A);
        Debug.Log(A.someInt);
        In_Test(in A);
        Debug.Log(A.someInt);
        ChangeObject(ref A);
        Debug.Log(A.someInt);
    }
}
