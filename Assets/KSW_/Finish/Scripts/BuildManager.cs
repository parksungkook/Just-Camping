using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BuildManager : MonoBehaviour
{
	public GameObject tent;

	public BuildSystem buildSystem;

	public line rightHand;
	public Joint joint;

	// Update is called once per frame
	void Update()
	{
		// 키보드로 할때
		//if (Input.GetKeyDown(KeyCode.H) && !buildSystem.isBuilding)
		//{
		//	buildSystem.NewBuild(tent);
		//}
		//tent.transform.position = new Vector3(transform.localScale.x, 0, transform.localScale.z);

		if (Input.GetKeyDown(KeyCode.H))
		{
			rightHand.canBuildTent = true;						
		}
		else if (Input.GetKeyDown(KeyCode.G)) // cancle
		{
			rightHand.canBuildTent = false;
		}
		
		//만약 텐트생성이 가능할때
		if(rightHand.canBuildTent)
		{
			if(buildSystem.isBuilding == true)
            {
				if(Input.GetMouseButtonDown(0) || joint.clickTrigger)
                {
					buildSystem.Build();
					rightHand.ResetClickTouchPad();					
				}
            }
			else
            {
				buildSystem.NewBuild(tent);				
            }
		}
		else 
        {
			buildSystem.CancleBuild();
		}

		//컴퓨터테스트시 끄기
		if(buildSystem.isBuilding)
        {
			buildSystem.DoBuildRay(rightHand.transform.position, rightHand.transform.forward);
        }
	}
}
