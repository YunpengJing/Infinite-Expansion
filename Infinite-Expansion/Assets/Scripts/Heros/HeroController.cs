using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
	public float speed;
	Vector3 movement;
	// Start is called before the first frame update
	void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update()
	{
		ProcessInputs();
		Move();
	}

	private void ProcessInputs()
	{
		//movement = new Vector3(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"), 0.0f);
		movement = new Vector3(SimpleInput.GetAxis("Horizontal"), 0.0f, SimpleInput.GetAxis("Vertical"));
		if (movement.magnitude > 1.0f)
		{
			movement.Normalize();
		}
	}

	private void Move()
	{
		transform.position = transform.position + movement * speed * Time.deltaTime;
	}

	
}
