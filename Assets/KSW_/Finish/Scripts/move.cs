using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
	private float speed = 5;
	private Vector3 dir;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		dir = h * Vector3.right + v * Vector3.up;
		transform.position += dir * speed * Time.deltaTime;
	}
}
