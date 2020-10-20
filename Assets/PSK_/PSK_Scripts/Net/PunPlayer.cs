using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PunPlayer : MonoBehaviourPun, IPunObservable
{
    public GameObject camRig;
    public GameObject virtualRig;
    public GameObject rightHand;
    public GameObject leftHand;

    public GameObject virtualRightHand;
    public GameObject virtualleftHand;

    public line rightHandLine;
    Vector3 otherPos;
    Quaternion otherRot;

    Vector3 otherPosL;
    Quaternion otherRotL;

    Vector3 otherPosR;
    Quaternion otherRotR;

    void Start()
    {
        if (photonView.IsMine) //Net 나일 경우만
        {
            camRig.SetActive(true);
            BuildManager bm = GameObject.Find("Manager/BuildingSystem").GetComponent<BuildManager>(); //굿코드
            bm.rightHand = rightHandLine;
            virtualRig.GetComponent<MeshRenderer>().material.color = Color.red; //굿코드
        }
        else
        {
            virtualRig.SetActive(true);
            virtualRig.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

    // Update is called once per frame
    void Update()
    {//Net 테스트
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    if (photonView.IsMine)  //Net 나일경우만
        //    {
        //        CreateBall();
        //        //photonView.RPC("RpcCreateBall", RpcTarget.All);   //RPC로 할경우       내가 만들고 뿌린다
        //    }
        //}

        if (photonView.IsMine == false) //Net 내가 아닐때 
        {
            transform.position = Vector3.Lerp(transform.position, otherPos, 0.5f);//Net 부드럽게
            transform.rotation = Quaternion.Lerp(transform.rotation, otherRot, 0.5f);

            //rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, otherPos, 0.5f); //Net 오른손 이동 부드럽게
            //rightHand.transform.rotation = Quaternion.Lerp(rightHand.transform.rotation, otherRot, 0.5f); //Net 오른손 회전 부드럽게

            //leftHand.transform.position = Vector3.Lerp(leftHand.transform.position, otherPos, 0.5f); //Net 왼손 이동 부드럽게
            //leftHand.transform.rotation = Quaternion.Lerp(leftHand.transform.rotation, otherRot, 0.5f); //Net 왼손 회전 부드럽게
        }
    }
    //Net동작공유
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 내거면 IsWriting 이 true -> 보내자
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position); //Net 나의 포지션과
            stream.SendNext(transform.rotation); //Net 나의 회전값

            stream.SendNext(rightHand.transform.position); //Net나의 오른손 위치
            stream.SendNext(rightHand.transform.rotation); //Net나의 오른손 회전값

            stream.SendNext(leftHand.transform.position); //Net나의 왼손 위치
            stream.SendNext(leftHand.transform.rotation); //Net나의 왼손 회전값
        }
        // 내것이 아니면 IsReading 이 true -> 읽자
        else if (stream.IsReading)
        {          // Net받는 형태  
            otherPos = (Vector3)stream.ReceiveNext();  //Net 마스터만 동작
            otherRot = (Quaternion)stream.ReceiveNext();    //Net 나의 회전값      
            virtualRightHand.transform.position = (Vector3)stream.ReceiveNext();//Net 보여줄 오른손 위치
            virtualRightHand.transform.rotation = (Quaternion)stream.ReceiveNext(); //Net 보여줄 오른손 회전값

            virtualleftHand.transform.position = (Vector3)stream.ReceiveNext();//Net 보여줄 왼손 위치
            virtualleftHand.transform.rotation = (Quaternion)stream.ReceiveNext(); //Net 보여줄 왼손 회전값
        }
    }
    //[PunRPC]  //Net RPC 사용개시
    //void RpcCreateBall()
    //{
    //    CreateBall();
    //}
    void CreateBall()
    {
        //GameObject ball = Instantiate(ballFactory);  
        //ball.transform.position = transform.position;
        PhotonNetwork.Instantiate("Ball", transform.position, Quaternion.identity); //Net 볼을 내자신의 자리에
    }

    GameObject objectInHand; ////Net 손
    [PunRPC] ////Net RPC사용할 함수의 위에 작성
    void RpcGrapObject(int viewId, int handType)
    {
        print(viewId + ", " + handType);

        objectInHand = GameManager.instance.FindMeat(viewId); //Net 고기를 찾고


        if (objectInHand == null) return; //Net 아무것도 없으면 다시 리턴

        //운동법칙 꺼주자
        objectInHand.GetComponent<Rigidbody>().isKinematic = true;

        if (handType == 0)//Net 만약 왼손이면
        {
            if(photonView.IsMine)//Net 가 나라면
            {
                objectInHand.transform.parent = leftHand.transform; //Net 왼손컨트롤러의 자식으로
            }
            else //Net 아니면 왼손 모형 컨트롤러의 자식으로
            {
                objectInHand.transform.parent = virtualleftHand.transform;
            }
        }
        else
        {
            if (photonView.IsMine)//Net 가 나라면
            {
                objectInHand.transform.parent = rightHand.transform; //Net 오른손컨트롤러의 자식으로
            }
            else //Net 아니면 오른손 모형 컨트롤러의 자식으로
            {
                objectInHand.transform.parent = virtualRightHand.transform;
            }
        }
    }

    [PunRPC]
    void RpcReleaseObject(Vector3 pos, Vector3 velocity, Vector3 angularVelocity)
    {
        if (objectInHand == null) return;

        //부모 없애자
        objectInHand.transform.parent = null;

        objectInHand.transform.position = pos;

        //중력 켜주자
        Rigidbody rb = objectInHand.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = velocity*2;//controllerPose.GetVelocity();
        rb.angularVelocity = angularVelocity;// controllerPose.GetAngularVelocity();

        objectInHand = null;
    }
}
