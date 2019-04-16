using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
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
