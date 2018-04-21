using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerPatrol : MonoBehaviour {

    public enum PatrolDirection {

        HORIZONTAL, VERTICAL
    }

    //public variables
    public float waitTimeBetweenPatrols = 2;
    public PatrolDirection patrolDirection = PatrolDirection.HORIZONTAL;
    public float patrolDistance = 1;
    public float speed = 10;

    //private variables
    private float timeSinceLastWait;
    private bool moving;
    private float distanceTraveled;
    private bool firstPatrol;

    void Start() {

        timeSinceLastWait = 0f;
        moving = false;
        distanceTraveled = 0;
        firstPatrol = true;
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
            }
        }
    }

    private float Move() {

        Vector2 patrolVector = new Vector2();
        switch (patrolDirection) {
            case PatrolDirection.HORIZONTAL:
                patrolVector = Vector2.right;
                break;
            case PatrolDirection.VERTICAL:
                patrolVector = Vector2.up;
                break;
        }

        Vector3 translation = new Vector3(patrolVector.x * speed * Time.deltaTime, patrolVector.y * speed * Time.deltaTime, 0f);
        transform.Translate(translation);
        return translation.magnitude;
    }

    private void Flip() {

        transform.Rotate(180, 180, 0);
    }
}
