using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Meat : MonoBehaviourPun
{
    public MeshRenderer mr;
    //public Material wait;
    public Material normal;
    public Material perfect;
    public Material fail;

    

    private float perfectTime = 0;
    private float middleTime = 4;

    public bool startTime;
    float time = 10;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.meatList.Add(photonView); //Net 생성되면 고기 리스트에 추가하자

        mr = GetComponent<MeshRenderer>();
        startTime =false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime == true)
        {               
            GrillMeat();
            time -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag.Contains("Grill"))
        {
            startTime = true;
            print("요리시작");
        }
    }
    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag.Contains("Grill"))
        {
            startTime = false;
            print("요리끝");
        }
    }
    public  void GrillMeat()
    {
        //원래상태로 있다가
        if(time >= middleTime)
        {
            print("익는중");
            mr.material = normal;
        }
        else if (time >= perfectTime && time<middleTime)
        {
            print("익었다");
            mr.material = perfect;
        }
        else if(time < perfectTime)
        {
            print("탔다");
            mr.material = fail;
            time = 0;            
        }
    }
}
