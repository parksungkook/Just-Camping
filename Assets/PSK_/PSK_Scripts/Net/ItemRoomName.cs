using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 버튼이 클릭되면 내가 가진 방 이름을 InputFieldRoomName.text에 대입하고싶다.
public class ItemRoomName : MonoBehaviour
{
    public string roomName;
    public Text textItem;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetRoomName(string roomName)
    {
        this.roomName = roomName;
        // 버튼의 text도 방이름으로 변경하고싶다.
        textItem.text = roomName;
    }

    public void OnClickMySelf()
    {
        //print("OnClickMySelf");
        LobbyManager.Instance.inputFieldRoomName.text = roomName;
    }
}
