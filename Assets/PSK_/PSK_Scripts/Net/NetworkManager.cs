using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//포톤 서버에 접속하고싶다.
//방에 입장하고싶다.
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject[] spawnPos;
    private void Start()
    {
        int randIdx = Random.Range(0, 4);

        // 입장하면 스폰위치중 랜덤한 곳으로 스폰 
        PhotonNetwork.Instantiate("[CameraRig]", spawnPos[randIdx].transform.position, Quaternion.identity);
        
       
    }

    //public Text textLog;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    textLog.text = "";
    //    //설정을 하고 포톤서버에 연결하고싶다.
    //    PhotonNetwork.GameVersion = "1.0.0";
    //    PhotonNetwork.SendRate = 30;
    //    PhotonNetwork.SerializationRate = 30;

    //    //보내기
    //    PhotonNetwork.ConnectUsingSettings();
    //    textLog.text += "ConnectUsingSettings" + "\n";

    //}

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    textLog.text += "OnDisconnected" + "\n";
    //    base.OnDisconnected(cause);
    //}
    ////받기
    //public override void OnConnectedToMaster()
    //{
    //    textLog.text += "OnConnectedToMaster" + "\n";

    //    base.OnConnectedToMaster();
    //    PhotonNetwork.JoinLobby();
    //}

    //public override void OnJoinedLobby()
    //{
    //    textLog.text += "OnJoinedLobby" + "\n";
    //    base.OnJoinedLobby();
    //    //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions() { MaxPlayers = 10, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    //    PhotonNetwork.JoinRoom("room");
    //}

    //public override void OnJoinRoomFailed(short returnCode, string message)
    //{
    //    textLog.text += "OnJoinRoomFailed" + "\n";
    //    base.OnJoinRoomFailed(returnCode, message);

    //    PhotonNetwork.CreateRoom("room", new RoomOptions() { MaxPlayers = 10, IsOpen = true, IsVisible = true });

    //}

    //public override void OnCreateRoomFailed(short returnCode, string message)
    //{
    //    textLog.text += "OnCreateRoomFailed" + "\n";
    //    base.OnCreateRoomFailed(returnCode, message);
    //}
    //public override void OnJoinedRoom()
    //{
    //    textLog.text += "OnJoinedRoom" + "\n";
    //    base.OnJoinedRoom();

    //    //플레이어를 만들고 그것을 생성하고싶다.
    //    PhotonNetwork.Instantiate("[CameraRig]", new Vector3(0, 1.0f, 0), Quaternion.identity);
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //}
}
