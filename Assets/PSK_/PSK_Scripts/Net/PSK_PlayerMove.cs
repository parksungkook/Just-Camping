using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PSK_PlayerMove : MonoBehaviourPun
{
	public float speed = 5.0f;
	private Vector3 dir;
	private CharacterController cc;

	// Start is called before the first frame update
	void Start()
	{if(photonView.IsMine)
        {

		cc = GetComponent<CharacterController>();
        }
	}

	// Update is called once per frame
	void Update()
	{if (!photonView.IsMine) return;
        

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		dir = new Vector3(h, 0, v);
		dir.Normalize();

		dir = Camera.main.transform.TransformDirection(dir);
		dir.y = 0;

		cc.Move(dir * speed * Time.deltaTime);
        
	}
}
