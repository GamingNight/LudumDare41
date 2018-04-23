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
    public float passKickSpeed = 2;
    public float shootMaxPower;

    private Camera cam;
    private GameObject ball;
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
    private float shootCharge;
    private float indivBoostMax;
    private Vector2 addvelocity;
    private int i;
    private float boostUsed;
    private bool isMainPlayer;


    // Sounds
    private AudioSource[] sounds;
    private AudioSource footstepSFX;
    private AudioSource passSFX;


    void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        rgbdBall = ball.GetComponent<Rigidbody2D>();
        ballMagnetism = ball.GetComponent<BallMagnetism>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        iStamina = stamina;
        indivBoost = 0;
        indivBoostMax = 33;
        clock = 0;
        animState = AnimationState.IDLE;
        i = 0;
        boostUsed = boost;
        isMainPlayer = true;

        // Sounds
        sounds = GetComponents<AudioSource>();
        footstepSFX = sounds[0];
        passSFX = sounds[1];
    }

    void FixedUpdate() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.V) && iStamina > 0 && indivBoost < indivBoostMax) {
            //Handle dash (interrupt walking)
            indivBoost = indivBoost + 1;
            indivBoost = Mathf.Min(indivBoost, indivBoostMax - 1);
            boostUsed = boost * (indivBoost / indivBoostMax + 1);
            Dash();
            clock = 0;
            if (indivBoost / indivBoostMax > 0.3) {
                ballMagnetism.amplitude = 1 - (indivBoost / indivBoostMax);
            }
        }
        if (Input.GetKeyUp(KeyCode.V)) {
            iStamina = iStamina - indivBoost;
            if (ballMagnetism.enabled) {
                addvelocity = new Vector2(movement.x * Mathf.Abs(rgbd.velocity.x), movement.y * Mathf.Abs(rgbd.velocity.y));
                rgbdBall.velocity = rgbd.velocity + addvelocity * strength * boostUsed;
                ballMagnetism.amplitude = 1;
            }
            if (indivBoost / indivBoostMax > 0.3) {
                i = i + 1;
                ballMagnetism.enabled = false;
                animator.SetBool("hasBall", false);
            }
            indivBoost = 0;
            boostUsed = boost;
        }
        if (!Input.GetKey(KeyCode.V)) {
            indivBoost = 0;
            if (clock < 3) {
                clock = clock + Time.deltaTime;
            } else {
                clock = 0;
                iStamina = iStamina + indivBoostMax / 2;
                if (iStamina > stamina) {
                    iStamina = stamina;
                }
            }
        }
        //Handle regular walking
        if (!Input.GetKey(KeyCode.R)) {
            Move(horizontal, vertical);
        }

        //Handle pass
        bool isMoving = horizontal != 0 || vertical != 0;
        if (Input.GetKey(KeyCode.Space) && isMoving && ballMagnetism.enabled) {
            Pass(horizontal, vertical);
        }
        //Handle Shoot charge
        if (Input.GetKey(KeyCode.R) && ballMagnetism.enabled) {
            shootCharge = shootCharge + 1;
            shootCharge = Mathf.Min(shootCharge, shootMaxPower);
            PlayerManager.GetInstance().ScoringPoints = shootCharge;
            animator.SetInteger("shootState", (int)1);
        }
        //Handle Shoot
        if (Input.GetKeyUp(KeyCode.R) && ballMagnetism.enabled) {
            cam.GetComponent<CameraFollow>().target = ball.transform;
            animator.SetInteger("shootState", (int)2);
            Shoot(shootCharge / shootMaxPower);
            shootCharge = 0;
        }
    }

    private void Move(float horizontal, float vertical) {

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

            // Sound
            if (!footstepSFX.isPlaying)
            {
                footstepSFX.Play();
            }

            spriteRenderer.flipX = horizontal < 0;
        } else {
            animState = AnimationState.IDLE;

            // Sound
            if (footstepSFX.isPlaying)
            {
                footstepSFX.Stop();
            }

        }
        animator.SetInteger("walkState", (int)animState);
    }

    private void Dash() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0) {
            movement.Set(horizontal, vertical);
            rgbd.AddForce(movement.normalized * strength * boostUsed);
        }
    }

    private void Shoot(float shootStrength) {

        // Sound
        if (!passSFX.isPlaying)
        {
            passSFX.volume = 0.6f;
            passSFX.Play();
        }

        ball.GetComponent<BallController>().dragBall = false;
        ballMagnetism.enabled = false;// la balle n'est plus aimantée à ce player
        ball.GetComponent<ShootTrajectory>().enabled = true;
        ball.GetComponent<ShootTrajectory>().shootStrength = shootStrength;
    }

    private void Pass(float horizontal, float vertical) {

        // Sound
        if (!passSFX.isPlaying)
        {
            passSFX.volume = 0.3f;
            passSFX.Play();
        }

        foreach (Transform child in transform) {
            if (child.tag == "PlayerCollider") {
                child.GetComponent<CircleCollider2D>().enabled = false; //un player qui est joueur n'a pas de zone de freinage de balle
            }
        }
        rgbdBall.drag = 0.3f;
        ball.GetComponent<BallController>().dragBall = false;
        rgbdBall.velocity = passKickSpeed * new Vector2(horizontal, vertical); // la balle est lancée
        GetComponent<PlayerController>().enabled = false; // le player n'est plus le joueur anymore
        ballMagnetism.enabled = false;// la balle n'est plus aimantée à ce player
        //Ce personnage n'est plus le player désormais
        PlayerManager.GetInstance().SetAllyAsNewPlayer();
    }

    public void SetBall(GameObject b) {
        ball = b;
    }

    public void SetCam(Camera c) {
        cam = c;
    }

    public bool IsMainPlayer() {
        return isMainPlayer;
    }

    public void SetMainPlayer(bool mainPlayer) {
        isMainPlayer = mainPlayer;
    }
}
