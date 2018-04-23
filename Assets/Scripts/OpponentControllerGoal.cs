using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentControllerGoal : MonoBehaviour {

    public GameObject fieldOfViewTrigger;
    private GameObject player;

    public void UpdatePlayer(GameObject newPlayer) {

        player = newPlayer;
    }

    void Update() {

        float newY = Mathf.Min(1.67f, Mathf.Max(0.845f, player.transform.position.y));
        float ratio = (1.67f - newY) / (1.67f - 0.845f);
        float newX = Mathf.Lerp(7.19f, 7.476f, ratio);
        transform.position = new Vector3(newX, newY, transform.position.z);

        Vector3 difference = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        fieldOfViewTrigger.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
