using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerRound : MonoBehaviour {

    //public variables
    public float movementFrequency = 1;
    public bool clockwise = true;
    public float angleStep = 90;

    //private variables
    private float timeSinceLastMove;

    void Start() {
        timeSinceLastMove = 0;
    }

    void Update() {

        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove >= movementFrequency) {
            Move();
            timeSinceLastMove = 0;
        }
    }

    private void Move() {

        transform.Rotate(0, 0, clockwise ? angleStep : -angleStep);
    }
}
