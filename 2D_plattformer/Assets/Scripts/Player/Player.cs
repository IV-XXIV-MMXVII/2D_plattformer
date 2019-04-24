using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float speed; //Speed of movement
    public float maxSpeed; //Our max speed
    public float setMaxSpeed;//max speed set point. 
    public float jumpForce; //The amount of jump

    bool step = false;

    public int totalJumps;//jump total 
    public static int __totalJumps;
    [HideInInspector] public int SetValueOfJjumps;//number of jumps. 
    public static int __SetValueOfJumps;

    Transform player;

    [HideInInspector] public static bool grounded = false;//not on ground. 

    [HideInInspector] public bool isWalking;//walking


    [HideInInspector] public static Animator __animator; //Our player's animator
   public Animator animator;


    Rigidbody2D rb; //Our rigidbody 2D

    [Header("Ground Check")]
    public Collider2D groundCheck;//collider 2D stops player from just falling through map. 

    [Header("Key Mapping")]
    //Map movement to a selected key
    public KeyCode right = KeyCode.RightArrow;//moves player forward. 
    public KeyCode left = KeyCode.LeftArrow;//moves player backward. 
    public KeyCode jump = KeyCode.Z;//allows player to jump. 

    //Flipping the image with this enumerator
    [HideInInspector] public enum DIRECTION
    {
        LEFT = -1,//speed moving backward. 
        RIGHT = 1//speed moving forward. 
    }

    //Events
    [Header("Events")]
    [Space]

    IEnumerator coroutine;

    private void Awake()
    {

        DontDestroyOnLoad(this);
    //    Camera_Follow.camera.target = gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<AudioManager>().Play("Music");
        rb = GetComponent<Rigidbody2D>();
        __animator = GetComponent<Animator>();
        animator = __animator;
        player = GetComponent<Transform>();
        SetValueOfJjumps = totalJumps;
        setMaxSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Set up bool for animator
        animator.SetBool("isWalking", isWalking);//This thread animates jumping for the player character so when it jumps it can make certain animations. This is the bool for that. 
        Debug.Log("Is Grounded" + grounded);
        Debug.Log("Set Value of Jumps: " + SetValueOfJjumps);
        Debug.Log("Total Jumps Left: " + totalJumps);

        if (Input.GetKey(right))
            MoveRight();
        else if (Input.GetKey(left))
            MoveLeft();
        else isWalking = false;

        if ((Input.GetKeyDown(jump) && totalJumps != 0) && (Input.GetKey(right) == false && Input.GetKey(left) == false))
            Jump();

        if (grounded == true)
        {
            totalJumps = SetValueOfJjumps;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.AdjustHealth(-1, 2);
        }
    }

    //Moving to the right
    public void MoveRight()
    {
        coroutine = Walk();//walking

        isWalking = true;//when is walking. 

        Flip(DIRECTION.RIGHT);//flips direction. 

        if (rb.velocity.magnitude < maxSpeed) {
            rb.velocity += new Vector2(speed, 0);//speed of walking. 
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(jump) && totalJumps != 0)//jump total. 
            Jump();
    }

    //Moving to the left
    public void MoveLeft()//moving left. 
    {
        coroutine = Walk();//walking left. 

        isWalking = true;

        Flip(DIRECTION.LEFT);//flips direction. 

        if (rb.velocity.magnitude < maxSpeed)//max speed. 
        {
            rb.velocity += new Vector2(-speed, 0);//speed of walking. 
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(jump) && totalJumps != 0)
            Jump();
    }

    //Jumping
    public void Jump()//jumping. 
    {
        coroutine = Walk();
        StopCoroutine(coroutine);
        maxSpeed = maxSpeed * 2;//speed of jump. 
        grounded = false;
        animator.SetBool("grounded", grounded);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //FindObjectOfType<AudioManager>().Play("Jump");
        --totalJumps;
    }

    //Flip the image over the y axis
    public void Flip(DIRECTION direction)
    {
        Vector3 xscale;
        switch (direction)
        {
            case DIRECTION.RIGHT://flips image on axis when moving right. 
                xscale = gameObject.transform.localScale;
                xscale.x = (float)DIRECTION.RIGHT;
                gameObject.transform.localScale = xscale;
                break;

            case DIRECTION.LEFT://flips image on axis when moving left. 
                xscale = gameObject.transform.localScale;
                xscale.x = (float)DIRECTION.LEFT;
                gameObject.transform.localScale = xscale;
                break;
        }
    }

    public IEnumerator Walk()
    {
        //This produces footstep noises as we move
        if (step == false)
        {
            FindObjectOfType<AudioManager>().Play("Walk");
            step = true;
            yield return new WaitForSeconds((float)0.15);
            step = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")//This allows for bullet from turret to kill player. 
        {
            GameManager.instance.AdjustHealth(-1, 5f);
        } else if (collision.gameObject.tag == "Deadzone")
        {
            GameManager.instance.AdjustHealth(-1, 100f);
        }
        else if (collision.gameObject.tag == "Mine")//this allows mine to do damage to player. 
        {
            GameManager.instance.AdjustHealth(-1, 25f);
        }
    }
}
