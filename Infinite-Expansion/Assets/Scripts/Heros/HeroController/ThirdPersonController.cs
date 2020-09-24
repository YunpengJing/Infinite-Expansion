using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private float heroSpeed = 5.0f;

    private CharacterController controller;
    private HeroInputManager heroInput;
    private Transform cameraMain;

    void Start()
    {
        cameraMain = Camera.main.transform;
    }

    private void Awake()
    {
        heroInput = new HeroInputManager();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        heroInput.Enable();
    }

    private void OnDisable()
    {
        heroInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = heroInput.HeroAction.Move.ReadValue<Vector2>();
        Vector3 move = (cameraMain.forward * moveInput.y + cameraMain.right * moveInput.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * heroSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }
}
