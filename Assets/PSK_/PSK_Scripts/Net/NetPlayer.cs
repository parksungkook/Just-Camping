using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetPlayer : MonoBehaviourPun, IPunObservable
{
    public GameObject cam;
    public GameObject cube;
    public GameObject ballFactory;

    Vector3 otherPos;
    Quaternion otherRot;

    // Start is called before the first frame update
    void Start()
    {
        // 만약에 내것이 아니라면
        if(photonView.IsMine == false)
        {
            cam.SetActive(false);
            GetComponent<PlayerMove>().enabled = false;
            cube.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            cube.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(photonView.IsMine)  //Net 나일경우만
            {
                CreateBall();
                //photonView.RPC("RpcCreateBall", RpcTarget.All);   //RPC로 할경우       내가 만들고 뿌린다
            }
        }
        
        if(photonView.IsMine == false) //Net 내가 아닐때 
        { 
            transform.position = Vector3.Lerp(transform.position, otherPos, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, otherRot, 0.5f);
        }        
    }


    [PunRPC]  //Net RPC 사용개시
    void RpcCreateBall()
    {
        CreateBall();
    }

    void CreateBall()
    {
        //GameObject ball = Instantiate(ballFactory);  
        //ball.transform.position = transform.position;
        PhotonNetwork.Instantiate("Ball", transform.position, Quaternion.identity); //Net 볼을 내자신의 자리에
    }


    //Net동작공유
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)  
    {
        // 내거면 IsWriting 이 true -> 보내자
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position); //Net 나의 포지션과
            stream.SendNext(transform.rotation); //Net 나의 회전값
        }
        // 내것이 아니면 IsReading 이 true -> 읽자
        else if(stream.IsReading)
        {          // Net받는 형태  
            otherPos = (Vector3)stream.ReceiveNext();  //Net 마스터만 동작
            otherRot = (Quaternion)stream.ReceiveNext();     //Net 나의 회전값      
        }
    }
}
