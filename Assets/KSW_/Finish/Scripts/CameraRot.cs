using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just Camping 강석원
public class CameraRot : MonoBehaviour
{
	private float rotSpeed = 200;
	private float angleX;
	private float angleY;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		angleX += mouseY * rotSpeed * Time.deltaTime;
		angleY += mouseX * rotSpeed * Time.deltaTime;

		angleX = Mathf.Clamp(angleX, -45, 45);

		Vector3 angle = new Vector3(-angleX, angleY, 0);

		transform.eulerAngles = angle;
	}
}
