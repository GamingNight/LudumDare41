using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum AnimationState {
        IDLE = 0, SIDE_WALK = 1, BACK_WALK = -1, FRONT_WALK = 2
    }

    public float strength;
    public float boost;
    public float stamina;

    private Rigidbody2D rgbd;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float drag;
    private float i;
    private float iStamina;
    private float indivBoost;
    private float clock;
    private AnimationState animState;

    void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        iStamina = stamina;
        indivBoost = stamina;
        clock = 0;
        animState = AnimationState.IDLE;
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
        }
        UpdateAnimation(horizontal, vertical);
    }

    private void UpdateAnimation(float horizontal, float vertical) {

        if (horizontal != 0 || vertical != 0) {
            if (vertical < 0 && horizontal == 0) {
                animState = AnimationState.FRONT_WALK;
            } else if (vertical > 0 && horizontal == 0) {
                animState = AnimationState.BACK_WALK;
            } else if (horizontal != 0) {
                animState = AnimationState.SIDE_WALK;
            }
            spriteRenderer.flipX = horizontal < 0;
        } else {
            animState = AnimationState.IDLE;
        }
        animator.SetInteger("walkState", (int)animState);
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
