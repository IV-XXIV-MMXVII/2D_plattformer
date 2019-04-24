using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour// all code on this page is usless. I decided to go a different rout with my enemies. instead of finding player the enemies sit still and shoot in a single line!!!!!! 
    //this makes it harder for the player to platform as they also must dodge the enemy fire wile trying to plattfom. I wanted to keep this code in so you could see how i decided to change this!!!!!!!
{
    public float speed;
    public float distance;

    private bool movingRight = true;

    private void Update()
    {

         transform.Translate(Vector2.right * speed);
                                                    
      
    }

    private void OnTriggerStay2D(Collider2D groundInfo)
    {
        if (groundInfo.gameObject.name == "trigger")
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        if (groundInfo.gameObject.name == "GroundCheck")
        {
            Destroy(this.gameObject);
        }
    }
}
