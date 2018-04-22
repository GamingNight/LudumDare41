using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public GameObject player;
    public float speed;
    public float slowBall;

    private Rigidbody2D rgbd;
    private BallMagnetism ballMagnetism;
    private bool pass;
    private bool dragBall = false;
    private float dragInit;

    void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        ballMagnetism = GetComponent<BallMagnetism>();
        dragInit = rgbd.drag;
        Debug.Log(dragInit);
    }

    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isMoving = horizontal != 0 || vertical != 0;
        if (Input.GetKey(KeyCode.Space) && isMoving && ballMagnetism.enabled) {
            pass = true;
        } else {
            pass = false;
        }
        if (pass) {
            rgbd.drag = 0.3f;
            rgbd.velocity = speed * new Vector2(horizontal, vertical); // la balle est lancée
            player.GetComponent<PlayerController>().enabled = false; // le player n'est plus le joueur anymore
            ballMagnetism.enabled = false;// la balle n'est plus aimantée à ce player
        }
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
        }
        if (other.gameObject.tag == "PlayerCollider" && !player.GetComponent<PlayerController>().enabled) {
            dragBall = true; // activation du freinage de la balle
            other.transform.parent.GetComponent<PlayerController>().enabled = true; // le receveur est le nouveau joueur
            ballMagnetism.player = other.transform.parent.gameObject; // la balle est aimantée à ce player
            player = other.transform.parent.gameObject; // le nouveau player de ce script est officiellement ce player.
        }
    }
}
