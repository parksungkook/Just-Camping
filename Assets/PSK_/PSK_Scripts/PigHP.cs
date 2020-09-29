using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//체력을 표현하고 싶다.
//도끼와 부딪히면 체력을 1 소모하게 하고싶다.
//총알과 부딪치면 체력을 3 소모하게 하고싶다.
//체력이 0이되면 죽고 싶다.
public class PigHP : MonoBehaviour
{
    public static PigHP instance;
    public void Awake()
    {
        instance = this;
    }

    public int maxHP = 5; //최대체력 
    public int curHP;  //현재체력
    public int newHP = 1;
    //고기공장
    public GameObject meatFactory;

    //private bool isDie = false;

    //현재시간
    float currentTime;
    //체력회복시간
    public float newHealthTime = 3f;

    bool dead;  //죽은상태
    public Slider sliderHP;//체력UI(Slider)
    public int HP
    {
        get { return curHP; }
        set
        {
            curHP = value;
            //체력이 계속 변환
            sliderHP.value = curHP;
        }
    }


    void Start()
    {
        //태어날때 현재체력을 최대체력으로 하고싶다.
        curHP = maxHP;
        //UI도 최대/현재체력을 표시하고 싶다.
        sliderHP.maxValue = maxHP;
        sliderHP.value = curHP;
        dead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (curHP > 0)
        {

            RestoreHealth();
        }
        //else
        //{
        //    OnFinishDie();
        //}

    }
    public void OnFinishDie()
    {        
        //현재 돼지수를 감소
        RespawnManager.instance.pigCount--;
        print(" 죽었다2");
        dead = true;
        Destroy(gameObject, 2.2f);
        Invoke("OnMeat", 2);

    }

    public void OnMeat()
    {
        GameObject meat = Instantiate(meatFactory);
        meat.transform.position = transform.position;
    }



    //체력을 회복 시키자
    internal void RestoreHealth()
    {//죽은상태에선 X
        if (dead == true)
        {
            return;
        }
        //만약 현제체력이 max체력보다 작다면
        else if (curHP < maxHP)
        {
            currentTime += Time.deltaTime; //시간이 흐르다가
            if (currentTime > newHealthTime) //회복시간이되면
            {
                curHP += newHP; //회복량만큼 체력을 증가시키자
                sliderHP.value = curHP;  //슬라이더도 갱신
                currentTime = 0; //시간 초기화
            }

        }
        //시간이 흐르다가
        //회복시간이 되면

        //회복시간이되면
        //체력을 1증가시키자

    }
    //public void PigDie()
    //{
    //    isDie = true;
    //}
}
