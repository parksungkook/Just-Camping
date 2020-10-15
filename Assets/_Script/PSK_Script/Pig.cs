using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Nav를 이용해  target 방향으로 이동시키자
// 상태를 만들자. Idle, Walk, Run, Attack
public class Pig : MonoBehaviour
{
    //상태제어
    enum State
    {
        Idle,
        Walk,
        Run
    }
    State state;


    //target찾기
    NavMeshAgent agent;
    GameObject target; //목표지정
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
        SetState(State.Idle);
    }
    // Update is called once per frame
    void Update()
    {
        //상태제어
        if (state == State.Idle)//대기
        {
            UpdateIdle();
        }
        else if (state == State.Walk)//걷기
        {
            UpdateWalk();
        }
        else if (state == State.Run)//뛰기
        {
            UpdateRun();
        }
    }
    private void UpdateRun()
    {
        agent.destination = target.transform.position;
    }
    private void UpdateWalk()
    {
        agent.speed = 2f;
    }
    private void UpdateIdle()
    {
        target = GameObject.Find("Player");
        if (target != null)
        {
            //상태전이
            //state = State.Run;
            //agent.speed = 3.5f;
            SetState(State.Run);
        }
    }
    //상태 전이기능
    void SetState(State next)
    {//상태를 다음상태로 전이하고
        state = next;
        //전이후에 같이 변경될 내용을 추가로 처리하고싶다.
        if(next==State.Run)
        {

        agent.speed = 3.5f;
        }
        else
        {
            agent.speed = 0;
        }
    }
}
