using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Sibal : MonoBehaviour
{//만약 스크립트가 꺼져있으면
    //키고싶다.
    public GameObject Rig;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        GetComponent<SteamVR_PlayArea>().enabled = true;
    }
}
