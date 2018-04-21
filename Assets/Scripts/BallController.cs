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
        //Debug.Log("A dragball update = " + dragBall);
        if (Input.GetKey(KeyCode.Space) == true)
        {
            pass = true;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            rgbd.velocity = speed * new Vector2(horizontal, vertical); // la balle est lancée
            player.GetComponent<PlayerController>().enabled = false; // le player n'est plus le joueur anymore
            GetComponent<BallMagnetism>().enabled = false;// la balle n'est plus aimantée à ce player
        }
        else
        {
            pass = false;
        }
        if (dragBall == true) { rgbd.drag = rgbd.drag + slowBall * Time.deltaTime; }// la balle est freinée dans le child du receveur
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<BallMagnetism>().enabled = true; //le receveur est aimanté à la balle
            dragBall = false; // le freinage de la balle est éteint
            rgbd.drag = 0; //idem
        }
        if (other.gameObject.tag == "PlayerCollider")
        {
            dragBall = true; // activation du freinage de la balle
            other.transform.parent.GetComponent<PlayerController>().enabled = true; // le receveur est le nouveau joueur
            GetComponent<BallMagnetism>().player = other.transform.parent.gameObject; // la balle est aimantée à ce player
            player = other.transform.parent.gameObject; // le nouveau player de ce script est officiellement ce player.
        }
    }
}
