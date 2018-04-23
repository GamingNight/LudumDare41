using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShowBall : MonoBehaviour {

    public GameObject arrow;

    private GameObject ball;
    private BallMagnetism ballMagnetism;
    private PlayerController playerController;

    void Start() {

        playerController = transform.parent.GetComponent<PlayerController>();
    }

    void Update() {

        if (!ballMagnetism.enabled && playerController.IsMainPlayer()) {
            arrow.SetActive(true);
            Vector3 difference = ball.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        } else {
            arrow.SetActive(false);
        }
    }

    public void SetBall(GameObject b) {
        ball = b;
        ballMagnetism = ball.GetComponent<BallMagnetism>();
    }
}
