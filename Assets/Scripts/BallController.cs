using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float slowBall;

    private bool pass;
    private Rigidbody2D rgbd;
    private bool dragBall = false;

    // Use this for initialization
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space) == true && (horizontal != 0 || vertical != 0))
        {
            pass = true;
        }
        else
        {
            pass = false;
        }
        if (pass==true)
        { 
            rgbd.velocity = speed * new Vector2(horizontal, vertical); // la balle est lancée
            player.GetComponent<PlayerController>().enabled = false; // le player n'est plus le joueur anymore
            GetComponent<BallMagnetism>().enabled = false;// la balle n'est plus aimantée à ce player
        }
        if (dragBall == true) { rgbd.drag = rgbd.drag + slowBall * Time.deltaTime; }// la balle est freinée dans le child du receveur
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBallCapture")
        {
            GetComponent<BallMagnetism>().enabled = true; //le receveur est aimanté à la balle
            dragBall = false; // le freinage de la balle est éteint
            rgbd.drag = 0; //idem
            //pass = false;
        }
        if (other.gameObject.tag == "PlayerCollider" && player.GetComponent<PlayerController>().enabled == false)
        {
            Debug.Log("pommier");
            dragBall = true; // activation du freinage de la balle
            other.transform.parent.GetComponent<PlayerController>().enabled = true; // le receveur est le nouveau joueur
            GetComponent<BallMagnetism>().player = other.transform.parent.gameObject; // la balle est aimantée à ce player
            player = other.transform.parent.gameObject; // le nouveau player de ce script est officiellement ce player.
        }
    }
}
