using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            
            Player.grounded = true;//Grounds player so does not fall through ground. 
            Player.__animator.SetBool("grounded", Player.grounded);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            Player.grounded = false;//If not grounded player will fall. This meants on tile map. 
            Player.__animator.SetBool("grounded", Player.grounded);
        }
    }
}