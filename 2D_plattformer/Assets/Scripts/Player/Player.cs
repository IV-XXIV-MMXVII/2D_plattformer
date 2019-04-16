using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float speed; //Speed of movement
    public float maxSpeed; //Our max speed
    public float setMaxSpeed;
    public float jumpForce; //The amount of jump

    bool step = false;

    public int totalJumps;
    public static int __totalJumps;
    [HideInInspector] public int SetValueOfJjumps;
    public static int __SetValueOfJumps;

    Transform player;

    [HideInInspector] public static bool grounded = false;

    [HideInInspector] public bool isWalking;


    [HideInInspector] public static Animator __animator; //Our player's animator
   public Animator animator;


    Rigidbody2D rb; //Our rigidbody 2D

    [Header("Ground Check")]
    public Collider2D groundCheck;

    [Header("Key Mapping")]
    //Map movement to a selected key
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode jump = KeyCode.Z;

    //Flipping the image with this enumerator
    [HideInInspector] public enum DIRECTION
    {
        LEFT = -1,
        RIGHT = 1
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
        animator.SetBool("isWalking", isWalking);
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
        coroutine = Walk();

        isWalking = true;

        Flip(DIRECTION.RIGHT);

        if (rb.velocity.magnitude < maxSpeed) {
            rb.velocity += new Vector2(speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(jump) && totalJumps != 0)
            Jump();
    }

    //Moving to the left
    public void MoveLeft()
    {
        coroutine = Walk();

        isWalking = true;

        Flip(DIRECTION.LEFT);

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(-speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(jump) && totalJumps != 0)
            Jump();
    }

    //Jumping
    public void Jump()
    {
        coroutine = Walk();
        StopCoroutine(coroutine);
        maxSpeed = maxSpeed * 2;
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
            case DIRECTION.RIGHT:
                xscale = gameObject.transform.localScale;
                xscale.x = (float)DIRECTION.RIGHT;
                gameObject.transform.localScale = xscale;
                break;

            case DIRECTION.LEFT:
                xscale = gameObject.transform.localScale;
                xscale.x = (float)DIRECTION.LEFT;
                gameObject.transform.localScale = xscale;
                break;
        }
    }

    public IEnumerator Walk()
    {
        //This will produce footstep noises as we move
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
        if (collision.gameObject.tag == "Bullet")
        {
            GameManager.instance.healthUIParent.gameObject.SetActive(false);
            GameManager.instance.Scene_Name = "Lose_Screen";
            GameManager.instance.Goto_Scene(GameManager.instance.Scene_Name);
            Destroy(gameObject);
        }
    }
}
