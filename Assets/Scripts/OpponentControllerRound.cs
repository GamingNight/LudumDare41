using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerRound : MonoBehaviour {

    //public variables
    public float movementPeriod = 0.5f;
    public bool clockwise = true;
    public float angleStep = 90;

    //private variables
    private float timeSinceLastMove;

    void Start() {
        timeSinceLastMove = 0;
    }

    void Update() {

        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove >= movementPeriod) {
            Move();
            timeSinceLastMove = 0;
        }
    }

    private void Move() {

        transform.Rotate(0, 0, clockwise ? -angleStep : angleStep);
    }
}
