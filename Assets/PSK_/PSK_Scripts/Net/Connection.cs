using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;



//시작 버튼을 누르면-> 함수가 필요하다.
//- 닉네임을 적용하고싶다
//- Connection 시작하고 완료되면 로비씬으로
public class Connection : MonoBehaviourPunCallbacks
{
    public Button buttonConnect;
    public InputField inputFieldNickname;
    public GameObject roomList;
    // Start is called before the first frame update
    private void Awake()
    {
        Screen.SetResolution(640, 320, FullScreenMode.Windowed);
    }
    void Start()
    {
        //설정을 하고 포톤서버에 연결하고싶다.
        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.SendRate = 50 ;
        PhotonNetwork.SerializationRate = 50;
        roomList.SetActive(false);
    }

   
   public void  OnClickConnect()
    {
        buttonConnect.interactable = false;
        // 연결버튼이 눌렸다.
        // -닉네임을 적용하고싶다.
        PhotonNetwork.NickName = inputFieldNickname.text;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        roomList.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
