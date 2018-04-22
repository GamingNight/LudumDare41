using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerScanAngle : MonoBehaviour {

    public enum AnimationState {

        IDLE_FRONT_SIDE = 0, IDLE_FRONT = 1, IDLE_BACK = -1
    }

    //public variables
    public GameObject fieldOfViewTrigger;
    public float movementPeriod = 1;
    public float angleStep = 90;
    [Range(0, 360)]
    public float scanAngle = 360;
    public bool startClockwise = true;
    public float waitTimeBetweenScans = 1f;

    //private variables
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool clockwise;
    private float timeSinceLastMove;
    private float angleScannedSoFar;
    private float timeSinceLastWait;
    private float absoluteAngle;
    private AnimationState animState;

    void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        clockwise = startClockwise;
        timeSinceLastMove = 0f;
        angleScannedSoFar = 0f;
        timeSinceLastWait = 0f;
        absoluteAngle = Clamp0360(transform.eulerAngles.z + fieldOfViewTrigger.transform.localEulerAngles.z);
        animState = AnimationState.IDLE_FRONT_SIDE;
    }

    void Update() {

        timeSinceLastWait += Time.deltaTime;
        if (timeSinceLastWait >= waitTimeBetweenScans) {
            timeSinceLastMove += Time.deltaTime;
            if (timeSinceLastMove >= movementPeriod) {
                angleScannedSoFar += Move();
                timeSinceLastMove = 0f;
            }

            if (angleScannedSoFar >= scanAngle) {
                clockwise = !clockwise;
                angleScannedSoFar = 0f;
                timeSinceLastWait = 0f;
            }
        }
    }

    private float Move() {

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

        return Mathf.Abs(angleStep);
    }

    public static float Clamp0360(float eulerAngles) {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0) {
            result += 360f;
        }
        return result;
    }
}
