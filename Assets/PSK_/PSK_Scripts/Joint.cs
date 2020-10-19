using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Joint : MonoBehaviour
{
    public SteamVR_Input_Sources handType; // 모두 사용, 왼손, 오른손
    public SteamVR_Behaviour_Pose controllerPose; // 컨트롤러 정보
    public SteamVR_Action_Boolean grabAction;
    
    public GameObject collidingObject; // 현재 충돌중인 객체
    private GameObject objectInHand; // 플레이어가 잡은 객체

    bool grabb;  //잡는중
    line line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<line>();
    }

    public bool clickTrigger;
    // Update is called once per frame
    void Update()
    {
        // 잡는 버튼을 누를 때
        if (grabAction.GetLastStateDown(handType))
        {
            clickTrigger = true;

            if(line.canBuildTent == false)
            {
                if (collidingObject)
                { 
                    GrabObject();
                }
            }

            //만약 clickTouchPad가 true라면
          
            //그 위치에 텐트를 두고 싶다.
        }
        //잡는 버튼을 뗄 떼
        if (grabAction.GetLastStateUp(handType))
        {
            clickTrigger = false;
            if (objectInHand)
            {
                ReleaseObject();
            }
        }

    }

    // 충돌이 시작되는 순간
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    // 충돌중일 때
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }
    // 충돌이 끝나는 순간
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }
    // 충돌중인 객체로 설정
    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;

    }
    // 객체를 잡음
    private void GrabObject()
    {
        objectInHand = collidingObject; // 잡은 객체로 설정
        collidingObject = null; // 충돌 객체 해제

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        //  collidingObject.transform.position = objectInHand.transform.position;
    }
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }




    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        objectInHand = null;
    }
}
