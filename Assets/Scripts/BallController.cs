using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float speed;
    public float slowBall;

    private bool pass;
    private Rigidbody2D rgbd;
    private bool dragBall=false;

    // Use this for initialization
    void Start () {
        rgbd = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            pass=true;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            rgbd.velocity = speed*new Vector2 (horizontal,vertical);
        }
        else
        {
            pass = false;
        }
        if (pass == true)
        {
            GetComponent<BallMagnetism>().enabled = false;
        }
        if (dragBall == true) { rgbd.drag = rgbd.drag + slowBall * Time.deltaTime; }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<BallMagnetism>().enabled = true;
        }
        if (other.gameObject.tag == "Ball")
        {
            dragBall = true;
        }
    }
}
