using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrajectory : MonoBehaviour {

    public GameObject goalKeeper;
    public float shootStrength = 1;
    public float iCollision = 0;

    // Use this for initialization
    void Start() {
        float dist = (goalKeeper.transform.position - transform.position).magnitude;
        Debug.Log("distance"+dist);
        PlayerManager.GetInstance().SetScoringPoints(PlayerManager.GetInstance().GetScoringPoints() / Mathf.Pow(dist,1.13f));
    }

    // Update is called once per frame
    void Update() {
        float step = (shootStrength * 5 / (iCollision + 1) + 1) * Time.deltaTime;
        //Debug.Log(shootStrength);
        transform.position = Vector3.MoveTowards(transform.position, goalKeeper.transform.position, step);
    }
}
