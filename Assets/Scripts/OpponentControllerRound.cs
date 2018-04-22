using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerRound : MonoBehaviour {

    public enum AnimationState {

        IDLE_FRONT_SIDE = 0, IDLE_FRONT = 1, IDLE_BACK = -1
    }

    //public variables
    public GameObject fieldOfViewTrigger;
    public float movementPeriod = 0.5f;
    public bool clockwise = true;
    public float angleStep = 90;

    //private variables
    private float timeSinceLastMove;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AnimationState animState;
    private float absoluteAngle;

    void Start() {

        timeSinceLastMove = 0;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = AnimationState.IDLE_FRONT_SIDE;
        absoluteAngle = Clamp0360(transform.eulerAngles.z + fieldOfViewTrigger.transform.localEulerAngles.z);
    }

    void Update() {
        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove >= movementPeriod) {
            Move();
            timeSinceLastMove = 0;
        }
    }

    private void Move() {

        float angleChange = clockwise ? -angleStep : angleStep;
        fieldOfViewTrigger.transform.Rotate(0, 0, angleChange);
        absoluteAngle = Clamp0360(absoluteAngle + angleChange);
        if (absoluteAngle <= 45 || absoluteAngle >= 135 && absoluteAngle <= 225 || absoluteAngle >= 315) {
            animState = AnimationState.IDLE_FRONT_SIDE;
        } else if (absoluteAngle >= 225 && absoluteAngle <= 315) {
            animState = AnimationState.IDLE_FRONT;
        } else if (absoluteAngle >= 45 && absoluteAngle <= 135) {
            animState = AnimationState.IDLE_BACK;
        }
        animator.SetBool("walk", false);
        animator.SetInteger("direction", (int)animState);
        spriteRenderer.flipX = spriteRenderer.flipY ? !(absoluteAngle > 90 && absoluteAngle < 270) : (absoluteAngle > 90 && absoluteAngle < 270);
    }

    public static float Clamp0360(float eulerAngles) {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0) {
            result += 360f;
        }
        return result;
    }
}
