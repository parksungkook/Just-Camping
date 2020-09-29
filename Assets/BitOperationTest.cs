using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitOperationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("2의 10승은 " + (1 << 10).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
