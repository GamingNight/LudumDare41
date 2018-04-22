using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpeed : MonoBehaviour {

    public float speedUp;

    private Rigidbody2D rgbd;
    // Use this for initialization
    void Start () {
        rgbd = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        rgbd.velocity = rgbd.velocity + speedUp * new Vector2(1f,0f) * Time.deltaTime;

    }
}
