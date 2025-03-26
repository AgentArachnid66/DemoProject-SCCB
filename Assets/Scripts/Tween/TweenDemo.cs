using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TweenDemo : MonoBehaviour
{

    public UnityEngine.UI.Image Image;

    public SceneAsset scene;
    
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.color(Image.rectTransform, Color.red, 0.2f).setLoopPingPong();
        LTDescr test = Image.transform.LeanScale(new Vector3(1.5f, 1.5f), 1f).setEaseInOutCubic().setLoopPingPong();

        LeanTween.cancel(test.id);
    }

    private void updateValueExampleCallback(float obj)
    {
        Debug.Log($"Value Callback [{obj}]");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
