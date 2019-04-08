using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //movement speed. 
    public float maxSpeed; //max speed
    public float setMaxSpeed;
    public float jump; // distance you can jump

    bool step = false;

    public int totalJumps;
    public static int _totalJumps;
    [HideInInspector] public int SetValueOfJumps;
    public static int _SetValueOfJumps;

    Transform player;

    [HideInInspector] public static bool grounded = false;

    [HideInInspector] public bool isWalking;

    [HideInInspector] public static Animator _animator; //player animator
    Animator animator;


    Rigidbody2D rb; //player rigid body. 

    [Header("Ground Check")]
    public Collider2D groundCheck;

    [Header("Key Mapping")]
    //maps the movement to select keys. 
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode jumpkey = KeyCode.Space;

    //This enumerator flips the immage. 
    [HideInInspector] public enum DIRECTION
    {
        LEFT = -1,
        RIGHT = 1
    }

    //event
    [Header("Events")]
    [Space]

    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Music");
        rb = GetComponent<Ridgidbody2D>();
        __animator = GetComponent<Animator>();
        animator = __animator;
        player = GetComponent<Transform>();
        SetValueOfJumps = totalJumps;
        setMaxSpeed = maxSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        //Set up bool (animator).
        animator.SetBool("isWalking", isWalking);
        Debug.Log("Is Grounded" + grounded);
        Debug.Log("Set Value of Jumps: " + SetValueOfJumps);
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
            totalJumps = SetValueOfJumps;
        }
    }
    //Moving 
    public void MoveRight()
    {
        coroutine = Walk();

        isWalking = true;

        Flip(DIRECTION.RIGHT);

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(jump)
    }

    //Moving left
    public void MoveLeft()
    {
        coroutine = Walk()


        isWalking = true;

        Flip(DIRECTION.LEFT);

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(-speed, 0);
        }
        if (grounded == true) StartCouroutine(coroutine);

        if (Input.GetKeyDown(jump) && totalJumps != 0)
            jump();
    }

    //jumping
    public void Jump()
    {
        coroutine = Walk();
        StopCoroutine(coroutine);
        maxSpeed = maxSpeed * 2;
        grounded = false;
        animator.SetBool("grounded", grounded);

    }


}
