using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerPatrol : MonoBehaviour {

    public enum PatrolDirection {

        HORIZONTAL, VERTICAL
    }

    //public variables
    public GameObject fieldOfViewTrigger;
    public float waitTimeBetweenPatrols = 2;
    public PatrolDirection patrolDirection = PatrolDirection.HORIZONTAL;
    public float patrolDistance = 1;
    public float speed = 10;

    //private variables
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float timeSinceLastWait;
    private bool moving;
    private float distanceTraveled;
    private bool firstPatrol;
    private bool wayBack;

    void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeSinceLastWait = 0f;
        moving = false;
        distanceTraveled = 0;
        firstPatrol = true;
        wayBack = false;
    }

    void Update() {

        if (!moving) {
            timeSinceLastWait += Time.deltaTime;
            if (timeSinceLastWait >= waitTimeBetweenPatrols) {
                if (!firstPatrol)
                    Flip();
                firstPatrol = false;
                moving = true;
                timeSinceLastWait = 0;
            }
        }

        if (moving) {
            distanceTraveled += Move();
            if (distanceTraveled >= patrolDistance) {
                moving = false;
                distanceTraveled = 0;
                animator.SetBool("walk", false);
            }
        }
    }

    private float Move() {

        Vector2 patrolVector = new Vector2();
        switch (patrolDirection) {
            case PatrolDirection.HORIZONTAL:
                patrolVector = wayBack ? Vector2.left : Vector2.right;
                break;
            case PatrolDirection.VERTICAL:
                if (wayBack) {
                    if (fieldOfViewTrigger.transform.right.y == 1) {
                        patrolVector = Vector2.down;
                    } else {
                        patrolVector = Vector2.up;
                    }
                } else {
                    if (fieldOfViewTrigger.transform.right.y == 1) {
                        patrolVector = Vector2.up;
                    } else {
                        patrolVector = Vector2.down;
                    }
                }
                break;
        }

        Vector3 translation = new Vector3(patrolVector.x * speed * Time.deltaTime, patrolVector.y * speed * Time.deltaTime, 0f);
        animator.SetBool("walk", true);
        if (patrolDirection == PatrolDirection.HORIZONTAL) {
            animator.SetInteger("direction", 0);
        } else {
            if (patrolVector == Vector2.up) {
                animator.SetInteger("direction", -1);
            } else if(patrolVector == Vector2.down) {
                animator.SetInteger("direction", 1);
            }
        }
        transform.Translate(translation);
        return translation.magnitude;
    }

    private void Flip() {

        spriteRenderer.flipX = !spriteRenderer.flipX;
        fieldOfViewTrigger.transform.Rotate(180, 180, 0);
        wayBack = !wayBack;
    }
}