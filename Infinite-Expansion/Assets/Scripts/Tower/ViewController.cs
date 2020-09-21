using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public float speed = 1;
    public float mouseSpeed = 60;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h * speed, 0, v * speed) * Time.deltaTime, Space.World);

        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(0, 0, mouse * mouseSpeed) * Time.deltaTime);
    }
}
