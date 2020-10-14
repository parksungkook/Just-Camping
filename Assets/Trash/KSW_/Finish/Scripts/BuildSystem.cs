using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
	public Camera cam;
	public LayerMask layer;
	private GameObject previewGameObject = null;
	private Preview preview = null;

	public float stickTolerance = 1.5f;

	public bool isBuilding = false;
	private bool pauseBuilding = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R)) // rotate
		{
			previewGameObject.transform.Rotate(0, 90f, 0);
		}
        //if(Input.GetKeyDown(KeyCode.G)) // cancle
        //{
        //	CancleBuild();
        //}
        //if(Input.GetMouseButton(0) && isBuilding) // build
        //{
        //	if (preview.GetSnapped())
        //	{
        //		StopBuild();
        //	}
        //	else
        //	{
        //		Debug.Log("Not Snapped");
        //	}
        //}
        //컴퓨터 테스트때 켜기
        //if (isBuilding)
        //{
        //    if (pauseBuilding)
        //    {
        //        float mouseX = Input.GetAxis("Mouse X");
        //        float mouseY = Input.GetAxis("Mouse Y");

        //        if (Mathf.Abs(mouseX) >= stickTolerance || Mathf.Abs(mouseY) >= stickTolerance)
        //        {
        //            pauseBuilding = false;
        //        }
        //    }
        //    else
        //    {
        //        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //        DoBuildRay(ray.origin, ray.direction);
        //    }
        //}
    }
	
	//미리보기 상태
	public void NewBuild(GameObject go)
	{
		if (isBuilding == true) return;
		previewGameObject = Instantiate(go, Vector3.zero, Quaternion.identity);
		preview = previewGameObject.GetComponent<Preview>();
		isBuilding = true;
	}

	public void CancleBuild()
	{
		if (isBuilding == false) return;

		Destroy(previewGameObject);
		previewGameObject = null;
		preview = null;
		isBuilding = false;
	}

	public void StopBuild()
	{
		preview.Place();
		previewGameObject = null;
		preview = null;
		isBuilding = false;
	}

	public void PauseBuild(bool value)
	{
		pauseBuilding = value;
	}

	public void Build()
    {
		if (isBuilding == false) return;
		if (preview.GetSnapped())
		{
			StopBuild();
		}
		else
		{
			Debug.Log("Not Snapped");
		}
	}

	public void DoBuildRay(Vector3 pos, Vector3 forward)
	{
		Ray ray = new Ray(pos, forward);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 100f, layer))
		{
			float y = hit.point.y + (previewGameObject.transform.localScale.y / 2f);			
			previewGameObject.transform.position = new Vector3(hit.point.x, y, hit.point.z);
		}
	}
}
