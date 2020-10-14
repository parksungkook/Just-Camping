using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public Text textLog;
    public Transform spawnList;
    Transform[] spawns;
    private void Awake()
    {                         //자식중에 받아 올 수 있다
        spawns = spawnList.GetComponentsInChildren<Transform>();
    }
    int type;
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        textLog.text += "<color=#ffff00>" + newPlayer.NickName + "</color><color=#000000>님이 입장하셨습니다.</color> \n";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        textLog.text += otherPlayer.NickName + "님이 퇴장하셨습니다. \n";
    }
    // Start is called before the first frame update
    void Start()
    {//클라이언트에서 보내는 비율 1초에 30번(RPC)
        PhotonNetwork.SendRate = 50;
        //호출 비율 1초에 30번
        PhotonNetwork.SerializationRate = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
