using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCalculation : MonoBehaviour {

    public float winPoints;
	// Use this for initialization
	void Start () {
        winPoints = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball" && PlayerManager.GetInstance().ScoringPoints != 0)
        {
            if (PlayerManager.GetInstance().ScoringPoints > winPoints)
            {
                Debug.Log("YOU SCORED");
            }
            else
            {
                Debug.Log("The goal keeper catched the ball!");
            }
        }
    }
}
