using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{

    [SerializeField]
    private float heroSpeed = 5.0f;

    private CharacterController controller;
    private HeroInputManager heroInput;
    
    private Transform cameraMain;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;



    // Start is called before the first frame update
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
        // hero move
        Vector2 moveInput = heroInput.HeroAction.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        controller.Move(move * Time.deltaTime * heroSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // camera follow
        if (cameraMain)
        {
            Vector3 destionation = new Vector3(transform.position.x, cameraMain.position.y, transform.position.z);
            cameraMain.position = Vector3.SmoothDamp(cameraMain.position, destionation, ref velocity, dampTime);
        }
    }
}