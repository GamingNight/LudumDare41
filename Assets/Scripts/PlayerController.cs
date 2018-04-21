using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float strength;
    public float boost;
    public float stamina;


    private Rigidbody2D rgbd;
    private Vector2 movement;
    private float drag;
    private float i;
    private float iStamina;
    private float indivBoost;
    private float clock;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        iStamina = stamina;
        indivBoost = stamina;
        clock = 0;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.V) && iStamina > 0 && indivBoost > 0) {
            //Handle dash (interrupt walking)
            Dash();
            indivBoost = indivBoost - 1;
            if (indivBoost == stamina - 1) {
                iStamina = iStamina - 1;
            }
        }
        if (Input.GetKey(KeyCode.V) == false) {
            indivBoost = stamina;
            if (clock < 2) {
                clock = clock + Time.deltaTime;
            } else {
                clock = 0;
                iStamina = iStamina + 1;
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
            } else if (vertical > 0) {
                animator.SetInteger("walkState", -1);
            } else {
                animator.SetInteger("walkState", 1);
            }
            spriteRenderer.flipX = horizontal < 0 && vertical <= 0 || horizontal > 0 && vertical > 0;
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
        if (horizontal != 0 || vertical != 0) {
            movement.Set(horizontal, vertical);
            //            rgbd.velocity = rgbd.velocity + movement.normalized * boost;
            rgbd.AddForce(movement.normalized * strength * boost);
        }
        //    if (vertical < 0)
        //    {
        //        animator.SetInteger("walking", 1);
        //        lastWalkingAnimationState = 1;
        //    }
        //    else if (vertical > 0)
        //    {
        //        animator.SetInteger("walking", -1);
        //        lastWalkingAnimationState = -1;
        //    }
        //    else
        //    {
        //        animator.SetInteger("walking", lastWalkingAnimationState);
        //    }
        //    spriteRenderer.flipX = horizontal < 0;
        //    if (!walkAudioSource.isPlaying)
        //        walkAudioSource.Play();
        //}
        //else
        //{
        //    walkAudioSource.Stop();
        //    animator.SetInteger("walking", 0);
        //}
    }
}
