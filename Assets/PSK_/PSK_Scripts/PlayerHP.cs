using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//체력을 표현하고 싶다.
//시간이 흐르다가
//일정 시간이되면
//체력을 깎고싶다
//체력이 5이하로 떨어지면
//시야가 좁아지게 하고싶다.

public class PlayerHP : MonoBehaviour
{

    //일정시간
    public float creatTime = 2f;
    float currentTime;

    int curHP;//현제체력
    public int maxHP = 10; //최대체력
    //체력UI(Slider)
    public Slider sliderHP;

    bool isDead = false;
    public int HP
    {
        get { return curHP; }
        set {
            curHP = value;
            //체력계속변환
            sliderHP.value = curHP; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //태어날때 체력을 최대체력으로
        curHP = maxHP;
        //UI에도 체력을 표시하고 싶다.
        sliderHP.maxValue = maxHP;
        sliderHP.value = curHP;


        while(!isDead)
        {
            print("현재 체력" + maxHP);

            curHP = curHP - 1;
            if(curHP<=0)
            {
                isDead = true;
                print("배고프다");
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        //시간이 흐르다
        //일정시간이 흐르는동안 && 음식을 섭취하지 않으면 
        //체력을 1씩 떨어트리고 싶다
        //섭취하면 일정량 체력이 채워진다.(최대치는maxHP)

        //만약 체력이 5이하가 되면 
        //시야를 흐릿하게 하고싶다.
        //만약 체력이 3이하가 되면
        //시야 범위를 줄이고 싶다.
        //일정시간이 흐르면
        //체력을 5정도 채우는 음식이 나타난다.
    }
}
