
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;
using UnityEngine.SceneManagement;
using Valve.VR;

public class line : MonoBehaviour
{
    #region "컨트롤러"
    //public AudioSource ad;
    //public SteamVR_Input_Sources handType;//사용손
    //public SteamVR_Input_Sources LeftHand;//왼손
    //public SteamVR_Input_Sources RightHand;//오른손


    // 왼손컨트롤러
    public SteamVR_Input_Sources leftHand;

    // 오른손 컨트롤러 받아오기
    public SteamVR_Input_Sources rightHand;
    public SteamVR_Behaviour_Pose ControllerPose;//컨트롤러 정보
    public SteamVR_Action_Boolean teleportAction;//텔레포트
    //// 트랙패드의 터치 포지션 값 가져오기
    //public SteamVR_Action_Vector2 touchPos;
    //// 트랙패드가 터치 되었는지 확인하기 위한 불린 값
    //public SteamVR_Action_Boolean touchPad;
    //public SteamVR_Action_Boolean touchPadClick;

    #endregion



    //레이저
    public GameObject laserPrefab;//레이저 프리팹
    private GameObject laser;//레이저
    private Transform laserTransform;//레이저 위치
    private Vector3 hitPoint;//레이저가 부딪친곳

    //텔레포트
    public Transform cameraRigTransform;//CameraRig 트랜스폼
    public GameObject teleprotReticlePrefab;//텔레포트 레티클 프리팹
    private GameObject reticle;// 레티클
    private Transform TeleprotReticleTransform;//텔레포트 레티클 위치
    public Transform headTransform;//플레이어 머리위치
    public Vector3 teleportRaticleOffset;//바닥표면 위에 표시하는 offset
    public LayerMask teleportMask;
    //게임시작UI
    public LayerMask InGameMask;
    //Lobby
    public LayerMask LobbyMask;
    //볼
    public LayerMask ItemMask;
    //public LayerMask ReStart;
    public LayerMask Exit;


    //컨트롤러 UI
    public GameObject controllerImage;



    private bool shouldTeleport;//텔레포트가 가능한지의 여부
    private bool controllerImageCheck; //컨트롤러 이미지



    void Awake()
    {
        #region"-----=====컨트롤러=====-----"
        leftHand = SteamVR_Input_Sources.LeftHand;
        rightHand = SteamVR_Input_Sources.RightHand;
        //touchPos = SteamVR_Actions.default_TouchPadPosition;
        //touchPad = SteamVR_Actions.default_TouchPad;
        //touchPadClick = SteamVR_Actions.default_TouchPadClick;
        //trigger = SteamVR_Actions.default_GrabPinch;
        //menu = SteamVR_Actions.default_Menu;

        #endregion


    }

    // Start is called before the first frame update
    void Start()
    {
        //ad.Stop();
        //레이저 프리팹 생성
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        //텔레포트
        reticle = Instantiate(teleprotReticlePrefab);
        TeleprotReticleTransform = reticle.transform;
        reticle.SetActive(false);

        //컨트롤러UI 끄기
        controllerImage.SetActive(false);

    }

    RaycastHit hit;

    public bool canBuildTent;
    bool clickTouchPad;
    bool clickTouchPad2;

    //아무것도 닿지 않았을때 레이어를 비우기
    int hitLayer = -1;
    
    void Update()
    {
        //if (teleportAction.GetState(SteamVR_Input_Sources.LeftHand))
        if (teleportAction.GetState(leftHand))
        {
            if (Physics.Raycast(ControllerPose.transform.position, transform.forward, out hit, 100))
            {
                // print("a");
                hitPoint = hit.point;
                reticle.SetActive(false);
                //shouldTeleport = true;
                //TeleprotReticleTransform.position = hit.point;
                int layer = hit.transform.gameObject.layer;
                

                switch (layer)
                {
                    case 11:  //텔레포트 레이어
                        // to do
                        Ground();
                        break;
                }
            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }

        if(teleportAction.GetStateDown(rightHand))
        {
            //눌렸을때  다시누르면꺼지기
            clickTouchPad = !clickTouchPad;

            if(clickTouchPad)
            {
                if (Physics.Raycast(ControllerPose.transform.position, transform.forward, out hit, 100))
                {//닿은 레이어를 담기
                    hitLayer = hit.transform.gameObject.layer;
                    if(hitLayer == 11)
                    {
                        canBuildTent = true;
                    }
                }
            }
            //닿지 않으면 담긴 레이어 초기화
            else
            {
                ResetClickTouchPad();
            }
        }

        if (clickTouchPad)
        {
            if (Physics.Raycast(ControllerPose.transform.position, transform.forward, out hit, 100))
            {   
                // print("a");
                hitPoint = hit.point;
                ShowLaser(hit);
                reticle.SetActive(false);
                //shouldTeleport = true;
                //TeleprotReticleTransform.position = hit.point;
                //int layer = hit.transform.gameObject.layer;

                switch (hitLayer)
                {
                    case 15:  //Start
                        GameStart();
                        break;
                    case 10:  //Item
                        Item();
                        break;
                    case 12: //Lobby
                        Lobby();
                        break;
                    case 14: //Exit
                        ex();
                        break;
                    case 16: //Controll UI
                        Controll();
                        break;
                }
            }
        }

        if (teleportAction.GetStateUp(leftHand) && shouldTeleport)
        {
            if (hit.transform.gameObject.layer == 11)   //  땐 순간 hit의 레이어가 Ground일때만 텔레포트-
            {
                Teleport();
            }
        }
    }

    public void ResetClickTouchPad()
    {
        hitLayer = -1;
        canBuildTent = false;
        clickTouchPad = false;
    }


    private void Teleport()  //텔레포트
    {
        shouldTeleport = false;
        reticle.SetActive(false);
        //현실과의 오차를 줄이기위해
        Vector3 difference = cameraRigTransform.position - headTransform.position;

        difference.y = 0.1f;//이동 후 땅에 안박히기 위한 꿀팁
        cameraRigTransform.position = hitPoint + difference;
    }
    //레이저 보여주기
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(ControllerPose.transform.position, hitPoint, 0.5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }
    void OnGame()
    {
        SceneManager.LoadScene(2);
    }
    void OnLobby()
    {
        SceneManager.LoadScene(0);
    }
    void OnExit()
    {
        Application.Quit();
    }

    void Ground()
    {
        ShowLaser(hit);
        shouldTeleport = true;
        TeleprotReticleTransform.position = hit.point;
        reticle.SetActive(true);
    }
    void Lobby() //로비
    {
        hitPoint = hit.point;
        ShowLaser(hit);
        //ad.Play();
        Invoke("OnLobby", 1f);
    }
    void GameStart()  //시작
    {
        hitPoint = hit.point;
        ShowLaser(hit);
        //shouldTeleport = true;
        //ad.Play();
        Invoke("OnGame", 1f);
    }
    void Item()
    {
        hitPoint = hit.point;
        ShowLaser(hit);
        float dist = Vector3.Distance(hit.transform.position, transform.position);
        hit.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //shouldTeleport = true;
        if (dist > 0.1)
        {
            hit.transform.position = Vector3.Lerp(hit.transform.position, transform.position, Time.deltaTime);
        }
    }
    void Controll()
    {
        controllerImageCheck = !controllerImageCheck;
        //if(controllerImageCheck)
        //{
        //    controllerImage.SetActive(!controllerImage.activeSelf);

        //}
        if(controllerImageCheck)
        {
            controllerImage.SetActive(true);
        }
        else
        {
            controllerImage.SetActive(false);
        }
        

    }
    void ex()
    {
        hitPoint = hit.point;
        ShowLaser(hit);
        //ad.Play();
        Invoke("OnExit", 1f);
    }

}

