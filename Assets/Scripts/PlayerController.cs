using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rgbdBall;
    public float strength;
    public float boost;
    public float stamina;
    public float indivBoostMax;


    private Rigidbody2D rgbd;
    private Vector2 movement;
    private float drag;
    private float i;
    private float iStamina;
    private float indivBoost=0;
    private float clock;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int test=0;

    // Use this for initialization
    void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        iStamina = stamina;
        indivBoostMax = 100;
        clock = 0;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        //if (!Input.GetKey(KeyCode.D))
        //{
        //    test = test + 1;
        //    Debug.Log("pommier=" + test + "    " + !Input.GetKey(KeyCode.D));
        //}
        Debug.Log(rgbd.velocity.magnitude+"     "+indivBoost+"    "+rgbdBall.velocity.magnitude);
        if (Input.GetKey(KeyCode.V) && iStamina < 100000 && indivBoost < indivBoostMax) {
            //Handle dash (interrupt walking)
            indivBoost = indivBoost + 1;
            //Debug.Log(indivBoost + "pipi");
            Dash();
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            iStamina = iStamina - indivBoost;
            if (rgbdBall.GetComponent<BallMagnetism>().enabled == true)
            {
                //rgbdBall.AddForce(movement.normalized * strength * boost * 30);
                Vector2 addvelocity = new Vector2(movement.x * Mathf.Abs(rgbd.velocity.x), movement.y * Mathf.Abs(rgbd.velocity.y));
                rgbdBall.velocity = rgbd.velocity / 1.2f + addvelocity * strength* boost * (indivBoost / indivBoostMax)/3f;
            }
            indivBoost = 0;
            rgbdBall.GetComponent<BallMagnetism>().enabled = false;
        }
        if (Input.GetKey(KeyCode.V) == false)
        {
            if (clock < 2)
            {
                clock = clock + Time.deltaTime;
            }
            else
            {
                clock = 0;
                iStamina = iStamina + indivBoostMax;
                if (iStamina > stamina) { iStamina = stamina; }
            }
        }
        //Handle regular walking
        Move();
    }

    private void Move() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0) {
            movement.Set(horizontal, vertical);
            rgbd.AddForce(movement.normalized * strength);
            if (vertical < 0 && horizontal == 0) {
                animator.SetInteger("walkState", 2);
            } else if (vertical > 0 && horizontal == 0) {
                animator.SetInteger("walkState", -1);
            } else {
                animator.SetInteger("walkState", 1);
            }
            spriteRenderer.flipX = horizontal < 0 && vertical <= 0 || horizontal < 0 && vertical > 0;
            //    if (!walkAudioSource.isPlaying)
            //        walkAudioSource.Play();
        } else {
            //    walkAudioSource.Stop();
            animator.SetInteger("walkState", 0);
        }
    }

    private void Dash() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            movement.Set(horizontal, vertical);
            //            
            rgbd.AddForce(movement.normalized * strength * boost);
        }
    }
}
