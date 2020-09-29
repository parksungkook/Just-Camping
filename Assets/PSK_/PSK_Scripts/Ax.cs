using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour
{
    LineRenderer lr;
   
    public LayerMask Item;   //보물상자  
    public LayerMask Pig;   //돼지
    public LayerMask Mushroom;  //버섯

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

        //RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetButtonDown("Fire1"))
        //{

        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //lr.SetPosition(0,transform.position);

        //if (Physics.Raycast(ray, out hit, 100))
        //{
        //        //hitpoint = hit.point;
        //    int layer = hit.transform.gameObject.layer;
        //    //닿은곳의 정보를 알고싶다.
           
        //    lr.SetPosition(1, hit.point);
        //        if (layer == 9)  //Pig
        //        {
        //            print("꾸엑");
        //            PigHP pghp = hit.transform.GetComponent<PigHP>();
        //            print("꾸웨억");
        //            pghp.HP--;
        //            if (pghp.HP <= 0)
        //            {
        //                pghp.OnFinishDie();
        //            }
        //        }
        //        else if(layer==8) //버섯
        //        {
        //            Mushroom mr = hit.transform.GetComponent<Mushroom>();
        //            mr.GoInventory();
        //        }
        //        else if(layer==10)  //보물상자
        //        {
        //           Test test = hit.transform.GetComponent<Test>();
        //            test.PlzOpen();
        //        }
                //transform.position = hitInfo.point;
            }
        //else
        //{
        //    //안부딪힐때 두번째 점의 위치를 10M앞으로
        //    lr.SetPosition(1, ray.origin + ray.direction * 10);
        //    transform.position = ray.origin + ray.direction * 10;
        //}
        
        //Ray
    
    //컨트롤러에 닿으면
    //돼지의 체력을 1씩 감소시키고 싶다.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==9)  //돼지
        {
            print("꾸엑");
            PigHP pghp = other.GetComponent<PigHP>();
            print("꾸웨억");
            pghp.HP -= 1;
            if (pghp.HP <= 0)
            {
                pghp.OnFinishDie(); 
            }
        }

        else if(other.gameObject.layer==8)  //버섯
        {
            Mushroom mr = other.GetComponent<Mushroom>();
            mr.GoInventory();
        }

        else if(other.gameObject.layer==10)  //보물상자
        {
            PSK_Itrem test = other.GetComponent<PSK_Itrem>();
            test.PlzOpen();
        }
    }

    //도끼에 맞으면 1씩감소
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Pig")
    //    {
    //        PigHP pghp = other.gameObject.GetComponent<PigHP>();
    //        pghp.HP--;
    //        if (pghp.HP <= 0)
    //        {
    //            Destroy(other.gameObject);
    //        }
    //    }

    //}
}
