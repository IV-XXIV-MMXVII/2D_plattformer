using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletmove : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//grabs component. 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-8, 0);//how fast bullet moves forward. 
    }
}//the code above is for the bullet. It allows it to be shot out of the turret. 
