using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager Instance;

    public GameObject content;
    public GameObject itemRoomNameFactory;
    private void Awake()
    {
        Instance = this;
    }

    public InputField inputFieldRoomName;
    public InputField inputFieldMaxPlayer;
    // Start is called before the first frame update
    void Start()
    {
        inputFieldMaxPlayer.text = "8";
    }
    public void OnClickJoinRoom()
    {
        print("OnClickJoin");
        PhotonNetwork.JoinRoom(inputFieldRoomName.text);
    }
    public void OnClickCreatRoom()
    {
        string roomName = inputFieldRoomName.text;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = byte.Parse(inputFieldMaxPlayer.text);

        PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel(2);

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("OnRoomListUpdate");
        base.OnRoomListUpdate(roomList);
        // UI를 갱신을 하고싶다. 
        // 1. 기존 목록을 다 삭제(Destroy)
        DestroyUIItemAll();
        // 2. 룸리스트의 값을 갱신 -> 미리 캐싱을 하고있어야한다.
        RoomCacheUpdate(roomList);
        // 3. 최종적으로 만들어진 캐시 목록을 이용해서 UI를 구성한다.
        MakeUI();
    }
    private void DestroyUIItemAll()
    {
        // content의 자식을 모두 파괴한다.
        //int childCount = content.transform.childCount;
        //print(childCount);
        //for (int i = 0; i < childCount; i++)
        //{
        //    Destroy(content.transform.GetChild(i).gameObject);
        //}
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }
    Dictionary<string, RoomInfo> roomCache;
    private void RoomCacheUpdate(List<RoomInfo> roomList)
    {
        if (roomCache == null)
        {
            roomCache = new Dictionary<string, RoomInfo>();
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo roomInfo = roomList[i];
            string key = roomInfo.Name;
            // 만약 룸캐시에 방이름이 포함되어 있으면
            if (roomCache.ContainsKey(key))
            {
                // 룸리스트의 방이 지워야하는 대상이라면 
                if (roomInfo.RemovedFromList)
                {
                    // 룸캐시에서 삭제하고
                    roomCache.Remove(key);
                }
                else // 그렇지 않다면 갱신하고싶다.
                {
                    roomCache[key] = roomInfo;
                }
            }
            else
            {
                // 그렇지 않다면 신규방이니까 룸캐시에 추가하고싶다.
                roomCache.Add(roomInfo.Name, roomInfo);
            }
        }
    }
    private void MakeUI()
    {
        //foreach (KeyValuePair<string, RoomInfo> info in roomCache)
        //{
        //    GameObject item = Instantiate(itemRoomNameFactory);
        //    item.transform.parent = content.transform;
        //    item.GetComponent<ItemRoomName>().SetRoomName(info.Key);
        //}

        var e = roomCache.GetEnumerator();

        while (e.MoveNext())
        {
            GameObject item = Instantiate(itemRoomNameFactory);
            item.transform.parent = content.transform;
            item.GetComponent<ItemRoomName>().SetRoomName(e.Current.Key);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
