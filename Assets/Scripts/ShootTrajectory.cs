using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrajectory : MonoBehaviour {

    public GameObject goalKeeper;
    public float shootStrength=1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float step = (shootStrength*5 + 1) * Time.deltaTime;
        //Debug.Log(shootStrength);
        transform.position = Vector3.MoveTowards(transform.position, goalKeeper.transform.position, step);
    }
}
