using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public GameObject mushFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  internal void  GoInventory()
    { 
        //현재 버섯 수 감소
        RespawnManager.instance.mushCount--;
        //인벤토리로 이동하자
        print("Go인벤토뤼");

        
        GameObject mushs = Instantiate(mushFactory);
        mushs.transform.position = transform.position;
        Destroy(gameObject);
    }

}
