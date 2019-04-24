using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    //Enemy
    public enum AIStates
    {
        Idle,
        Chase,
        LookAround,
        GoHome,
        StartAlert,
        EndAlert
    }
    public float enemyHealth;
    public Transform playerPosition;
    public Vector3 homePoint;
    public Vector3 goalPoint;
    public AIStates currentState;
    public float giveUpChaseDistance;
    public float closeEnough;

    public float moveSpeed = 1;
    public float turnSpeed = 1;

    public float moveVolume = 5;
    public float turnVolume = 5;

    public static Vector3 originPosition;
    public Vector2 move;

    public Animator animator;

   

    public bool col = false, step = false;

    public bool isWaiting = false;

    public bool isWalking = false;

    //Initializing speed and the speed of rotation
    public float rateOfSpeed, maxSpeed;

    public bool fowardDown, backwardsDown, rightDown, leftDown;

    public Rigidbody2D rb; //Giving an identifier (or name) that'll reference our RigidBody!!

    public IEnumerator coroutine;

    public Transform tf;
   

    // Start is called before the first frame update
    public virtual void Start()//all code below is the player pawn. this game doesnt have an enemy pawn but does have one for player. 
    {


        tf = GetComponent<Transform>();

       

        homePoint = tf.position;
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void MoveFoward()
    {


    } //The player moves forward. 

    public virtual void MoveRight()
    {

    } //The player moves right

    public virtual void MoveBackwards()
    {

    } //The player moves Backwards. 

    public virtual void MoveLeft()
    {

    }//player moves left. 

    public IEnumerator Walk()
    {
        if (step == false)
        {
            step = true;
            yield return new WaitForSeconds((float)0.15);
            step = false;
        }
    }
    //impliments idol function 
    public virtual void Idle()
    {

    }
    //impliments chase function 
    public virtual void Chase()
    {

    }
    //impliments look around function 
    public virtual void LookAround()
    {

    }
    //impliments go home function 
    public virtual void GoHome()
    {

    }
    //impliments move forward function. 
    public virtual void MoveTowards(Vector3 target)
    {


    }
    public virtual void Move(Vector3 direction)
    {

    }
    //gives direction 
    public virtual void Turn(bool isTurnClockwise)
    {

    }
    public virtual void TakeDamage(int damage)
    {

    }
}//allows for turning. 
