using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderEnable : MonoBehaviour {

    public GameObject ball;
    private float distBall;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<PlayerController>().enabled == false)
        {
            distBall = (transform.position - ball.transform.position).magnitude;
            if (distBall > 0.6f)
            {
                foreach (Transform child in transform)
                {
                    if (child.tag == "PlayerCollider")
                    {
                        //Debug.Log("C. PlayerController of player is false => enable circle collider");
                        child.GetComponent<CircleCollider2D>().enabled = true; // un player qui n'est pas joueur a une zone de freinage de balle
                    }
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "PlayerCollider")
                {
                    //Debug.Log("C. PlayerController of player is false => enable circle collider");
                    child.GetComponent<CircleCollider2D>().enabled = false; //un player qui est joueur n'a pas de zone de freinage de balle
                }
            }
        }
    }
}
