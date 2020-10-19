using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Valve.VR;

public class Joint : MonoBehaviourPun, IPunObservable
{
    public SteamVR_Input_Sources handType; // 모두 사용, 왼손, 오른손
    public SteamVR_Behaviour_Pose controllerPose; // 컨트롤러 정보
    public SteamVR_Action_Boolean grabAction;
    
    public GameObject collidingObject; // 현재 충돌중인 객체
    private GameObject objectInHand; // 플레이어가 잡은 객체

    bool grabb;  //잡는중
    line line;

    Vector3 otherPos;
    Quaternion otherRot;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<line>();
    }

    public bool clickTrigger;
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return; //Net 내가아니라면
        
            // 잡는 버튼을 누를 때
            if (grabAction.GetLastStateDown(handType))
            {
                clickTrigger = true;

                if (line.canBuildTent == false)// 텐트가 소환된 상태가 아닐때
                {
                    if (collidingObject)
                    {
                    //photonView.RPC("GrabObject()", RpcTarget.All); //Net 생성 후 뿌리기
                        GrabObject();
                    }
                }
            }
            //만약 clickTouchPad가 true라면

            //그 위치에 텐트를 두고 싶다.
            {

            }

            //잡는 버튼을 뗄 떼
            if (grabAction.GetLastStateUp(handType))
            {
                clickTrigger = false;
                if (objectInHand)
                {
               // photonView.RPC("ReleaseObject()", RpcTarget.All); //Net 생성 후 뿌리기
               ReleaseObject();


                }
            }
        

    }
   // [PunRPC]
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

        PhotonView pv = collidingObject.transform.GetComponent<PhotonView>();//Net 만약 충돌된 객체가 포톤뷰를 갖고 있다면
        if(pv != null)
        {

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        }

        //  collidingObject.transform.position = objectInHand.transform.position;
    }
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }




    private void ReleaseObject()//놨을때
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
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 내거면 IsWriting 이 true -> 보내자
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position); //Net 나의 포지션과
            stream.SendNext(transform.rotation); //Net 나의 회전값

            stream.SendNext(objectInHand.transform.position); //Net잡힌 물체 위치
            stream.SendNext(objectInHand.transform.rotation); //Net잡힌 물체 회전값

           

        }
        // 내것이 아니면 IsReading 이 true -> 읽자
        else if (stream.IsReading)
        {          // Net받는 형태  
            otherPos = (Vector3)stream.ReceiveNext();  //Net 마스터만 동작
            otherRot = (Quaternion)stream.ReceiveNext();    //Net 나의 회전값      


            objectInHand.transform.position = (Vector3)stream.ReceiveNext();//Net 보여줄 잡힌 물체 위치
            objectInHand.transform.rotation = (Quaternion)stream.ReceiveNext(); //Net 보여줄 잡힌 물체 회전값
        }
    }
}
