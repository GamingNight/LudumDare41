using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum AnimationState {
        IDLE = 0, SIDE_WALK = 1, BACK_WALK = -1, FRONT_WALK = 2
    }

    public GameObject ball;
    public float strength;
    public float boost;
    public float stamina;
    public float indivBoostMax;

    private Rigidbody2D rgbd;
    private Rigidbody2D rgbdBall;
    private BallMagnetism ballMagnetism;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float drag;
    private float iStamina;
    private float indivBoost;
    private float clock;
    private AnimationState animState;

    void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        rgbdBall = ball.GetComponent<Rigidbody2D>();
        ballMagnetism = ball.GetComponent<BallMagnetism>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        iStamina = stamina;
        indivBoost = 0;
        indivBoostMax = 100;
        clock = 0;
        animState = AnimationState.IDLE;
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.V) && iStamina < 100000 && indivBoost < indivBoostMax) {
            //Handle dash (interrupt walking)
            indivBoost = indivBoost + 1;
            Dash();
        }
        if (Input.GetKeyUp(KeyCode.V)) {
            iStamina = iStamina - indivBoost;
            if (ballMagnetism.enabled) {
                Vector2 addvelocity = new Vector2(movement.x * Mathf.Abs(rgbd.velocity.x), movement.y * Mathf.Abs(rgbd.velocity.y));
                rgbdBall.velocity = rgbd.velocity / 1.2f + addvelocity * strength * boost * (indivBoost / indivBoostMax) / 3f;
            }
            indivBoost = 0;
            ballMagnetism.enabled = false;
        }
        if (!Input.GetKey(KeyCode.V)) {
            indivBoost = stamina;
            if (clock < 2) {
                clock = clock + Time.deltaTime;
            } else {
                clock = 0;
                iStamina = iStamina + indivBoostMax;
                if (iStamina > stamina) {
                    iStamina = stamina;
                }
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
            rgbd.AddForce(movement.normalized * strength * boost);
        }
    }
}
