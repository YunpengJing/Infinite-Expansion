using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestView : MonoBehaviour
{
    void Start()
    {
        CameraFeature.instance.StartUp(transform, 100);
    }
}
