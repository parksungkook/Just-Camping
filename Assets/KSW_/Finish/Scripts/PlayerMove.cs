using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float speed = 5.0f;
	private Vector3 dir;
	private CharacterController cc;

	// Start is called before the first frame update
	void Start()
	{
		cc = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		dir = new Vector3(h, 0, v);
		dir.Normalize();

		dir = Camera.main.transform.TransformDirection(dir);
		dir.y = 0;

		cc.Move(dir * speed * Time.deltaTime);
	}
}
