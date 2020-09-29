using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Player 와 Camera 의 회전을 담당하는 클래스
//Player는 좌, 우 회전만
//Camera는 상, 하 회전만
//위, 아래 시야는 -80,80
public class CameraRotate : MonoBehaviourPun
{
    CharacterController cc;
    //회전속도
    public float rotSpeed = 180;

    //회전값을 저장하는 변수
    Vector3 rotPlayer;
    // 카메라의 회전값을 저장하는 변수
    Vector3 rotCam;

    //Player Transform

    public Transform trPlayer;

    Quaternion camRot;


    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine) //Net 나일 경우만 
        {

            rotCam = transform.localEulerAngles;

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)//Net 나일 경우만 
        {
            if(!Cursor.visible)
            {

                //1. 마우스 좌우상하 입력받아서
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");

                //2. 그 값으로 각도를 갱신

                rotCam.y += x * rotSpeed * Time.deltaTime;
                rotCam.x -= y * rotSpeed * Time.deltaTime;

                 //3. -80 80의 값으로 세팅

                rotCam.x = Mathf.Clamp(rotCam.x, -80, 80);
                //그 값을 Player, Camera 세팅
                transform.localEulerAngles = rotCam;
                //4. 플레이어의 좌, 우 각도를 카메라의 움직임에 따라 바꾸자

                trPlayer.localEulerAngles = new Vector3(0, rotCam.y, 0);
                transform.parent.localEulerAngles = new Vector3(0, -rotCam.y, 0);
            }

        }

    }

//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        if(stream.IsWriting)
//        {

//        }
//        if(stream.IsReading)
//        {
//            camRot = (Quaternion)stream.ReceiveNext();
//;        }
//    }
}
