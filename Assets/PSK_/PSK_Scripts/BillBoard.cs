using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Vector3 offset;

    private void OnEnable() //켜져있을때마다
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        //비트연산 땅을 뚤지않게하기위해 ~0으로 
        if(Physics.Raycast(ray, out hitInfo, offset.z, ~0))
        {
            //Z파이팅 안겹치게
            Vector3 pos = hitInfo.point + hitInfo.normal * 0.01f;
            pos.y = Camera.main.transform.position.y;
            transform.position = pos;
            transform.forward = Camera.main.transform.forward;
        }
        else
        {
            Vector3 imagePos = Camera.main.transform.position + offset;

            transform.position = Camera.main.transform.TransformPoint(imagePos);
            transform.forward = Camera.main.transform.forward;
        }
    }


    void Update()
    {
        //transform.rotation = Camera.main.transform.rotation; // 나의 회전 = 메인카메라의 회전
        /*Vector3 temp = Camera.main.transform.forward;
        temp.y = 0;
        transform.forward = temp;*/
    }


}
