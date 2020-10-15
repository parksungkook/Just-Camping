using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

//타겟목록대로 지점을 순환하고 싶다.
//순찰상태
public class PigMove : MonoBehaviourPun
{   //움직이는상태
    //먹이를 찾아가는상태
    //도망치는상태

    public enum State
    {
        Patrol,
        Run,
        Eat,

    }
    public State state;
    //public WayPoint wayPoint;



    public int targetIndex;
    NavMeshAgent agent;
    public GameObject player;
    Transform target;
    //public LayerMask unitLayer;

    public Transform[] targets;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient == false) return; //Net 마스터만 동작

        agent = GetComponent<NavMeshAgent>();
        state = State.Patrol;

        //정해진 위치를 향해서 이동하고 싶다.
        //target = wayPoint.targets[targetIndex];
        target = targets[targetIndex];
        agent.destination = target.position;

        //int idx = 0;
        //targets = new Transform[4];
        //Waypoint 게임오브젝트의 transform
        //Transform tr = GameObject.Find("wayPont").transform;

        ////transform에 자식 갯수만 반복
        //foreach(Transform child in tr)
        //{
        //    targets[idx] = child;
        //    idx++;
        //    print(child.name);
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient == false) return;

        if (state == State.Patrol)
        {
            UpdatePatrol();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }


        //Vector3 direction = (transform.position - player.transform.position).normalized;
        //transform.position += direction * agent.speed * Time.deltaTime;

    }
    void UpdateRun()
    {
        //정해진 거리가 될때까지 반대방향으로 이동
        if ((transform.position - player.transform.position).magnitude < 10)
        {

            Vector3 direction = (transform.position - player.transform.position).normalized;
            agent.speed = 1.5f;
            transform.position += direction * agent.speed * Time.deltaTime;
            //print("ㅌㅌing");

        }
        else
        {
            agent.isStopped = false;
            state = State.Patrol;

        }

    }
    void UpdatePatrol()
    {//플레이어가 정해진 거리만큼 가까워지면
        if ((transform.position - player.transform.position).magnitude < 5)
        {
            print("으악 인간이다");
            agent.isStopped = true;
            state = State.Run;
        }

        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            print(" 먹을거없나꿀꿀");
            //다음 타겟 인덱스
            targetIndex++;
            //인덱스가 wayPoint.targets.Length를 넘어가지 않게 하기
            targetIndex = targetIndex % targets.Length; //4면->0
            //다음 다겟으로 destination 설정
            agent.destination = targets[targetIndex].position;
        }
    }



    //private void OnCollisionStay(Collision collision)
    //{
    //    //만약 내주변 일정범위 안에 플레이어가 머물러 있다면
    //    Collider[] cols = Physics.OverlapSphere(pigBody.transform.position, pigBody.transform.localScale.z / 2);
    //    for (int i = 0; i<cols.Length; i++)
    //    {
    //        if(cols[i].gameObject.tag=="Player")
    //        {//플레이어를 찾고
    //            target = GameObject.Find("Player").transform;
    //            //타겟 방향을 플레이어와 먼곳으로 바꾸고싶다.
    //            print("인간냄새가 나는데..");
    //            agent.destination = transform.position - player.transform.position;
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    //tag 부딪칠때마다 인덱스 값을 증가시키자
    //    if (other.gameObject.name.Contains("Target"))
    //    {
    //        targets[targetIndex].GetComponent<BoxCollider>().enabled = false;
    //        int max = targets.Length;

    //        print(other.name);
    //        ++targetIndex;
    //        //
    //        //int max
    //        //if (targetIndex >= max)
    //        //{
    //        //    targetIndex = 0;
    //        //}
    //        //or
    //        //targetIndex = targetIndex % wayPoint.targets.Length; //=>targetIndex%=wayPoint.targets.Length;

    //       targets[targetIndex].GetComponent<BoxCollider>().enabled = true;//
    //        agent.destination = targets[targetIndex].position;
    //    }
    //}
}
