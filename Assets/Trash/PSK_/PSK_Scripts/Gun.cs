using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun: MonoBehaviour
{
    public GameObject bulletFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateBomb();
    }
    void UpdateBomb()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            GameObject bullet = Instantiate(bulletFactory);
            //카메라 위치로
            bullet.transform.position = transform.position;
            //폭탄의 앞방향과 카메라 앞방향 일치
            bullet.transform.forward = Camera.main.transform.forward;
        }
    }
  
}
