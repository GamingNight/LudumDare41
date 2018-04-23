using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerPatrol : MonoBehaviour {

    public enum PatrolDirection {

        HORIZONTAL, VERTICAL
    }

    //public variables
    public GameObject fieldOfViewTrigger;
    public float initialWait = 0;
    public float waitTimeBetweenPatrols = 2;
    public PatrolDirection patrolDirection = PatrolDirection.HORIZONTAL;
    public float patrolDistance = 1;
    public float speed = 10;

    //private variables
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private float timerSinceLastWait;
    private bool moving;
    private float distanceTraveled;
    private bool firstPatrol;
    private bool wayBack;

    void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = 0f;
        timerSinceLastWait = 0f;
        moving = false;
        distanceTraveled = 0;
        firstPatrol = true;
        wayBack = false;
    }

    void Update() {

        timer += Time.deltaTime;
        if (timer < initialWait)
            return;

        if (!moving) {
            timerSinceLastWait += Time.deltaTime;
            if (timerSinceLastWait >= waitTimeBetweenPatrols) {
                if (!firstPatrol)
                    Flip();
                firstPatrol = false;
                moving = true;
                timerSinceLastWait = 0;
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
                if ((int)fieldOfViewTrigger.transform.right.x == 1) {
                    //vers la droite
                    patrolVector = Vector2.right;
                } else {
                    //vers la gauche
                    patrolVector = Vector2.left;
                }
                break;
            case PatrolDirection.VERTICAL:
                if ((int)fieldOfViewTrigger.transform.right.y == 1) {
                    //vers le haut
                    patrolVector = Vector2.up;
                } else {
                    //vers le bas
                    patrolVector = Vector2.down;
                }
                //}
                break;
        }

        Vector3 translation = new Vector3(patrolVector.x * speed * Time.deltaTime, patrolVector.y * speed * Time.deltaTime, 0f);
        animator.SetBool("walk", true);
        if (patrolDirection == PatrolDirection.HORIZONTAL) {
            animator.SetInteger("direction", 0);
        } else {
            if (patrolVector == Vector2.up) {
                animator.SetInteger("direction", -1);
            } else if (patrolVector == Vector2.down) {
                animator.SetInteger("direction", 1);
            }
        }
        transform.Translate(translation);
        return translation.magnitude;
    }

    private void Flip() {

        spriteRenderer.flipX = !spriteRenderer.flipX;
        fieldOfViewTrigger.transform.Rotate(0, 0, 180);
        wayBack = !wayBack;
    }
}