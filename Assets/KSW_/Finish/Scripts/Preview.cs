﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
	public GameObject prefab;

	private MeshRenderer myRend;
	public Material gootMat; // green
	public Material badMat; // red

	private BuildSystem buildSystem;

	private bool isSnapped = false;
	public bool isTent = false;

	public List<string> tagsISnapTo = new List<string>();

	void Start()
	{
		buildSystem = GameObject.FindObjectOfType<BuildSystem>();
		myRend = GetComponent<MeshRenderer>();
		ChangeColor();
	}

	public void Place()
	{
		Instantiate(prefab, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	private void ChangeColor()
	{
		if (isSnapped)
		{
			myRend.material = gootMat;
		}
		else
		{
			myRend.material = badMat;
		}
		if (isTent)
		{
			myRend.material = gootMat;
			isSnapped = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		for (int i = 0; i < tagsISnapTo.Count; i++)
		{
			string currentTag = tagsISnapTo[i];

			if (other.tag == currentTag)
			{
				buildSystem.PauseBuild(true);
				transform.position = other.transform.position;
				isSnapped = true;
				ChangeColor();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		for (int i = 0; i < tagsISnapTo.Count; i++)
		{
			string currentTag = tagsISnapTo[i];

			if (other.tag == currentTag)
			{
				isSnapped = false;
				ChangeColor();
			}
		}
	}

	public bool GetSnapped()
	{
		return isSnapped;
	}
}
