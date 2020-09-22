using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCameraLook : MonoBehaviour
{
    [SerializeField]
    private float lookSpeed = 1f;

    private CinemachineFreeLook cinemachine;
    private HeroInputManager heroInput;

    private void Awake()
    {
        heroInput = new HeroInputManager();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        heroInput.Enable();
    }

    private void OnDisable()
    {
        heroInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = heroInput.HeroAction.Look.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += delta.x * 200 * lookSpeed * Time.deltaTime;
        cinemachine.m_YAxis.Value += delta.y * lookSpeed * Time.deltaTime;
    }
}
