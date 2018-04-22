using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float slowBall;
    public bool dragBall = false;

    private GameObject player;
    private Rigidbody2D rgbd;
    private BallMagnetism ballMagnetism;
    private bool pass;
    private float dragInit;

    void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        ballMagnetism = GetComponent<BallMagnetism>();
        dragInit = rgbd.drag;
    }

    public void UpdatePlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    void Update() {
        if (dragBall) {
            // la balle est freinée dans le child du receveur
            rgbd.drag = rgbd.drag + slowBall * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerBallCapture") {
            ballMagnetism.enabled = true; //le receveur est aimanté à la balle
            dragBall = false; // le freinage de la balle est éteint
            rgbd.drag = dragInit; //remet le freinage à 0
            player.GetComponent<Animator>().SetBool("hasBall", true);
        }
        if (other.gameObject.tag == "PlayerCollider") {
            dragBall = true; // activation du freinage de la balle
        }
    }
}
