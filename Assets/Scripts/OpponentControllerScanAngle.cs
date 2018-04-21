using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerScanAngle : MonoBehaviour {

    //public variables
    public float movementPeriod = 1;
    public float angleStep = 90;
    [Range(0, 360)]
    public float scanAngle = 360;
    public bool startClockwise = true;
    public float waitTimeBetweenScans = 1f;

    //private variables
    private bool clockwise;
    private float timeSinceLastMove;
    private float angleScannedSoFar;
    private float timeSinceLastWait;

    void Start() {
        clockwise = startClockwise;
        timeSinceLastMove = 0f;
        angleScannedSoFar = 0f;
        timeSinceLastWait = 0f;
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

        transform.Rotate(0, 0, clockwise ? -angleStep : angleStep);
        return Mathf.Abs(angleStep);
    }
}
