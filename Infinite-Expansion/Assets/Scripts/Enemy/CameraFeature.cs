using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFeature : MonoBehaviour
{
    public static CameraFeature instance;

    public Vector3 offset;
    public float time_show;

    private Transform m_target;
    private Transform m_camera;
    private Vector3 m_position;
    private Vector3 m_destination;

    private float m_timer;
    private float speed;

    private bool _action;
    private bool action
    {
        get
        {
            return _action;
        }
        set
        {
            _action = value;

            if (_action)
            {
                Time.timeScale = 0.2f;
            }
            else
            {
                Time.timeScale = 1f;
            }

            gameObject.SetActive(action);
        }
    }

    private void Awake()
    {
        instance = this;

        m_camera = transform;
        m_position = transform.position;

        action = false;
    }

    private void Update()
    {
        if (action)
        {
            if (Vector3.Distance(m_camera.position, m_destination) < 0.1f)
            {
                m_camera.position = m_destination;
                m_timer += Time.deltaTime;

                if (m_timer > time_show * Time.timeScale)
                {
                    Finish();
                }
            }
            else
            {
                m_camera.LookAt(m_target);
                m_camera.position = Vector3.Lerp(m_camera.position, m_destination, speed * Time.deltaTime);
            }
        }
    }

    public void StartUp(Transform target, float time = 6)
    {
        m_timer = 0;
        m_target = target;
        m_destination = m_target.position + offset;
        Debug.Log(time);
        speed = Vector3.Distance(m_position, m_destination) / time;

        action = true;
    }

    private void Finish()
    {
        action = false;

        m_camera.position = m_position;
    }
}
