using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//일정시간마다 동물공장에서 멧돼지를 생성해 일정영역 안의 랜덤한 위치에 가져다 놓고 싶다.
public class RespawnManager : MonoBehaviour
{
    //RespawnManager 객체는 딱 1개만 존재해야한다.
    public static RespawnManager instance;
    private void Awake()
    {
        instance = this;
    }
    //공통조건
    float currentTime;   //현재시간
    public float CreatTime = 3f;   //생성시간
    float left, right, forward, back; //일정영역안의 랜덤한 위치


    public GameObject pigFactory;  //동물공장                               
    public int pigCount;   //현재 생성된 돼지 수
    public int maxPigCount = 3;  //최대 돼지 수

    //채집공장
    public GameObject mushroom;  //01버섯

    //채집생성시간
    public int mushCount;
    public int maxMushCount = 3;  //01 최대 생성 버섯 수

    //02 생성된 수

    //CharacterController cc;


    void Start()
    {
        // cc = GetComponent<CharacterController>();
        //태어날 때 적을 생성할 범위
        left = transform.position.x - transform.localScale.x / 2;
        right = transform.position.x + transform.localScale.x / 2;
        back = transform.position.z - transform.localScale.z / 2;
        forward = transform.position.z + transform.localScale.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient == false) return;//Net 마스터만 동작

        //만약 플레이어가
        //숲에 들어선다면
        UpdatePig();
    }
    void UpdatePig()
    {
        currentTime += Time.deltaTime;   //시간이 흐르다가
        if (currentTime > CreatTime)
        {
            currentTime = 0;   //시간초기화
            //랜덤한 위치설정
            Vector3 origin = new Vector3(Random.Range(left, right), transform.position.y, Random.Range(back, forward));
            //그 위치에서 아래방향으로 시선을 만든다
            Ray ray = new Ray(origin, Vector3.down);
            //만약 시선이 닿는곳이 있다면
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                //만약 현재 돼지 생성 수가 최대 생성수보다 작다면
                //다시 돼지를 생성한다.
                if (pigCount < maxPigCount)
                {
                    Vector3 pos = new Vector3(0, 2, 0);
                    pigCount++;    //돼지가 생성될때 현재 돼지 생성 수 1증가
                    print("증가했다");
                    //GameObject pig = Instantiate(pigFactory);   //동물공장에서 돼지를 생성해서                  
                    //pig.transform.position = hitInfo.point+pos;    //위치에 두고싶다
                    PhotonNetwork.Instantiate("Pig", hitInfo.point + pos, Quaternion.identity);
                }
                if (mushCount < maxMushCount)
                {
                    mushCount++;   // 
                    //돼지 생성과 겹치지 않기 위해 불규칙좌표생성
                    Vector3 pos = new Vector3(Random.Range(-10.0f, 10.0f), 0.55f, Random.Range(-10.0f, 10.0f));
                    GameObject mush = Instantiate(mushroom);  //버섯공장에서 버섯생성해서
                    mush.transform.position = hitInfo.point + pos;  //위치에둔다
                }


                //돼지가 파괴될때 1감소->pigHP에서 한다.
                //버섯이 파괴될때 1감소->Mushroom에서 한다.
            }
        }
    }
}
