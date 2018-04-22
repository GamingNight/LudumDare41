using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject player;
    public Camera cam;
    public GameObject ball;
    public GameObject PrefabPlayer;
    public float speed;

    private Rigidbody2D rgbd;
    private Rigidbody2D rgbdBall;
    private GameObject ally;
    private BallMagnetism ballMagnetism;
    private float horizontal;
    private float vertical;
	// Use this for initialization
	void Start () {
        rgbd = player.GetComponent<Rigidbody2D>();
        rgbdBall = ball.GetComponent<Rigidbody2D>();
        ballMagnetism = ball.GetComponent<BallMagnetism>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 pos = new Vector3(-1, 1, 0);
            ally = Instantiate<GameObject>(PrefabPlayer, player.transform.position + pos, Quaternion.identity);
            ally.GetComponent<PlayerColliderEnable>().ball = ball;
            ally.GetComponent<PlayerController>().ball = ball;
            ally.GetComponent<Rigidbody2D>().velocity = new Vector2(rgbd.velocity.x,0f);
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        bool isMoving = horizontal != 0 || vertical != 0;
        if (Input.GetKey(KeyCode.Space) && isMoving && ballMagnetism.enabled)
        {
            Pass();
        }



    }

    private void Pass()
    {
        foreach (Transform child in player.transform)
        {
            if (child.tag == "PlayerCollider")
            {
                //Debug.Log("C. PlayerController of player is false => enable circle collider");
                child.GetComponent<CircleCollider2D>().enabled = false; //un player qui est joueur n'a pas de zone de freinage de balle
            }
        }
        rgbdBall.drag = 0.3f;
        rgbdBall.velocity = speed * new Vector2(horizontal, vertical); // la balle est lancée
        player.GetComponent<PlayerController>().enabled = false; // le player n'est plus le joueur anymore
        ballMagnetism.enabled = false;// la balle n'est plus aimantée à ce player
        if (ally != null)
        {
            ally.GetComponent<AllySpeed>().enabled = false; // le receveur n'accélère plus tout seul
            ally.GetComponent<PlayerController>().enabled = true; // le receveur est le nouveau joueur
            cam.GetComponent<CameraFollow>().target = ally.transform;
            player = ally;
        }
        else
        {
            Debug.Log("Fin du Game, lol une passe sans allié");
        }
    }
}
