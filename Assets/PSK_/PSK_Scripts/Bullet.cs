using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {if(other.gameObject.tag=="Pig")
        {
            PigHP pgh = other.gameObject.GetComponent<PigHP>();
            pgh.HP--;
            if(pgh.HP<=0)
            {
                pgh.OnFinishDie();
            }
        }
        Destroy(gameObject);
    }
}
