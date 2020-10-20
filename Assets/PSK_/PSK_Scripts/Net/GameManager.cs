using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public Text textLog;
    public Transform spawnList;
    Transform[] spawns;

    //public PhotonView[] meats;
    public List<PhotonView> meatList = new List<PhotonView>(); //Net 새로 생성된 리스트, 리스트 초기화, 아직 제거는 안시킴
    //public List<PhotonView> zengaList = new List<PhotonView>(); //Net 새로 생성된 리스트, 리스트 초기화, 아직 제거는 안시킴

    private void Awake()
    {
        instance = this;
        //자식중에 받아 올 수 있다
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
    {//클라이언트에서 보내는 비율 1초에 50번(RPC)
        PhotonNetwork.SendRate = 50;
        //호출 비율 1초에 50번
        PhotonNetwork.SerializationRate = 50;
    }


    public GameObject FindMeat(int viewId)
    {
        //for(int i = 0; i < meats.Length; i++)//Net 배열 테스트
        //{
        //    if(meats[i].ViewID == viewId)
        //    {
        //        return meats[i].gameObject;
        //    }
        //}

        for(int i = 0; i < meatList.Count; i++)// Net 고기가 새로 생성될때마다  i 증가
        {
            if(meatList[i].ViewID == viewId)// Net 새로생성된 고기들을 리스트에 담자  
            {
                return meatList[i].gameObject; //Net 고기 게임오브젝트 반환
            }
        }
        return null;
    }
    //public GameObject FindZenga(int viewId)
    //{

    //for(int i=0; i<zengaList.Count;i++)
    //    {
    //        if(zengaList[i].ViewID==viewId)
    //        {
    //            return zengaList[i].gameObject;
    //        }
    //    }
    //}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) //버튼이 눌린다면 
        {
            PhotonNetwork.Instantiate("Meat_", new Vector3(76.35104f, 0.68f, 95), Quaternion.identity);//Net 정해진 위치에 고기를 두자
            PhotonNetwork.Instantiate("Zenga_", new Vector3(70f, 0.68f, 95), Quaternion.identity);//Net 정해진 위치에 젠가를 두자
        }
    }
}
