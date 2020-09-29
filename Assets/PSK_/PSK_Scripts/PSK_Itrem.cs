using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PSK_Itrem : MonoBehaviour
{
    public GameObject itemFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void PlzOpen()
    {

        Destroy(gameObject, 2);
        GameObject item = Instantiate(itemFactory);
        item.transform.position = transform.position;
    }
}
