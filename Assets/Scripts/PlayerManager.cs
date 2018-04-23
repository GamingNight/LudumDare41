using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager instance;

    public static PlayerManager GetInstance() {
        return instance;
    }

    public GameObject player;
    public Camera cam;
    public GameObject ball;
    public GameObject prefabPlayer;
    public float numPassMax;
    public Text lifeText;

    private GameObject ally;
    private float horizontal;
    private float vertical;
    private int allyNum;
    private float scoringPoints;

    private void Awake() {
        if (instance == null)
            instance = this;
        ShareANewPlayerHasCome(player);
        AssignObjectsToPlayer(player);
        allyNum = 0;
        lifeText.text = "" + numPassMax;
        scoringPoints = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            InstantiateAlly(true, Vector2.one);
        }
    }

    public void InstantiateAlly(bool autoMove, Vector2 ballDirection) {

        if (ally != null || allyNum >= numPassMax) {
            return;
        }

        //Compute spawn position
        Vector3 position = ComputeSpawnPosition(autoMove, ballDirection);

        //Camera unzoom to encompass player + ally
        cam.GetComponent<CameraZoom>().size = 1.4f;

        //Instantiate
        ally = Instantiate<GameObject>(prefabPlayer, position, Quaternion.identity);
        //change camera target
        cam.GetComponent<CameraFollow>().target = ally.transform;

        //Dig a hole in current player clouds to see ally
        foreach (Transform child in player.transform) {
            if (child.tag == "Clouds") {
                foreach (Transform childSZ in child.transform) {
                    if (childSZ.tag == "SafeZoneAlly3") {
                        childSZ.gameObject.SetActive(true);
                        childSZ.gameObject.GetComponent<FollowTheAlly>().ally = ally;
                        foreach (Transform childC in child.transform) {
                            if (childC.tag == "Clouds1") {
                                childC.GetComponent<ParticleSystem>().trigger.SetCollider(2, childSZ);
                            }
                        }
                    }
                    if (childSZ.tag == "SafeZoneAlly4") {
                        childSZ.gameObject.SetActive(true);
                        childSZ.gameObject.GetComponent<FollowTheAlly>().ally = ally;
                        foreach (Transform childC in child.transform) {
                            if (childC.tag == "Clouds2") {
                                childC.GetComponent<ParticleSystem>().trigger.SetCollider(2, childSZ);
                            }
                        }
                    }
                }
            }
        }

        //Give ally an initial velocity if not static
        if (autoMove)
            ally.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0f);

        //Give ally all the objects that he needs
        AssignObjectsToPlayer(ally);

        //Handle activation of scripts and components
        ally.GetComponent<PlayerController>().enabled = false;
        ally.GetComponent<AllySpeed>().enabled = true;
        foreach (Transform child in ally.transform) {
            if (child.gameObject.tag == "Clouds")
                child.gameObject.SetActive(false);
            if (child.gameObject.tag == "PlayerCollider")
                child.gameObject.SetActive(true);
        }

        //One more ally to the counter
        allyNum = allyNum + 1;
        lifeText.text = (numPassMax - allyNum) + "";
    }

    private Vector3 ComputeSpawnPosition(bool autoMove, Vector2 ballDirection) {

        Vector3 pos = Vector3.zero;
        if (autoMove) {
            Vector3 offset = Vector3.zero;
            if (player.transform.position.y < -3) {
                offset = new Vector3(-1, 1, 0);
            } else if (player.transform.position.y > 3.6) {
                offset = new Vector3(-1, -1, 0);
            } else {
                if (Random.value >= 0.5) {
                    offset = new Vector3(-1, 1, 0);
                } else {
                    offset = new Vector3(-1, -1, 0);
                }
            }
            pos = player.transform.position + offset;
        } else {

            Vector2 playerPosition = player.transform.position;
            Vector2 playerViewportPoint = cam.WorldToViewportPoint(playerPosition);
            Vector2 point2 = playerViewportPoint + ballDirection;

            Vector2 target = Vector2.one;
            if (ballDirection.x == 0) {
                target = cam.ViewportToWorldPoint(new Vector2(playerViewportPoint.x, ballDirection.y > 0 ? 1 : 0));
            } else if (ballDirection.y == 0) {
                target = cam.ViewportToWorldPoint(new Vector2(ballDirection.x > 0 ? 1 : 0, playerViewportPoint.y));
            } else {
                float a = (point2.y - playerViewportPoint.y) / (point2.x - playerViewportPoint.x);
                float b = playerViewportPoint.y - a * playerViewportPoint.x;

                float yTarget;
                float xTarget;
                if (Mathf.Abs(ballDirection.x) > Mathf.Abs(ballDirection.y)) {
                    xTarget = ballDirection.x > 0 ? 1 : 0;
                    yTarget = a * xTarget + b;
                } else {
                    yTarget = ballDirection.y > 0 ? 1 : 0;
                    xTarget = (yTarget - b) / a;
                }
                target = cam.ViewportToWorldPoint(new Vector2(xTarget, yTarget));
            }

            pos = new Vector3(target.x, target.y, 0);
        }
        return pos;
    }

    //Is called as soon as ally has received the ball
    public void SetAllyAsNewPlayer() {
        if (ally != null) {
            ally.GetComponent<AllySpeed>().enabled = false; // le receveur n'accélère plus tout seul
            ally.GetComponent<PlayerController>().enabled = true; // le receveur est le nouveau joueur
            foreach (Transform child in player.transform) {
                if (child.tag == "Clouds") {
                    child.gameObject.SetActive(false); //désactivation des nuages autour de l'ex player
                }
            }
            foreach (Transform child in ally.transform) {
                if (child.tag == "Clouds") {
                    child.gameObject.SetActive(true); //activation des nuages autour du nouveau
                }
            }
            //Dig a hole in current player clouds to see ally
            foreach (Transform child in ally.transform)
            {
                if (child.tag == "Clouds")
                {
                    foreach (Transform childSZ in child.transform)
                    {
                        if (childSZ.tag == "SafeZoneAlly3")
                        {
                            childSZ.gameObject.SetActive(true);
                            childSZ.gameObject.GetComponent<FollowTheAlly>().ally = null;
                            childSZ.transform.position = player.transform.position;
                            foreach (Transform childC in child.transform)
                            {
                                if (childC.tag == "Clouds1")
                                {
                                    childC.GetComponent<ParticleSystem>().trigger.SetCollider(2, childSZ);
                                }
                            }
                        }
                        if (childSZ.tag == "SafeZoneAlly4")
                        {
                            childSZ.gameObject.SetActive(true);
                            childSZ.gameObject.GetComponent<FollowTheAlly>().ally = null;
                            childSZ.transform.position = player.transform.position;
                            foreach (Transform childC in child.transform)
                            {
                                if (childC.tag == "Clouds2")
                                {
                                    childC.GetComponent<ParticleSystem>().trigger.SetCollider(2, childSZ);
                                }
                            }
                        }
                    }
                }
            }
            cam.GetComponent<CameraZoom>().size = 0.72f;
            ShareANewPlayerHasCome(ally);

            //Update player and ally variables
            GameObject oldPlayer = player;
            player = ally;
            ally = null;
            Destroy(oldPlayer);


        } else {
            Debug.Log("Fin du Game, lol une passe sans allié");
        }
    }

    private void ShareANewPlayerHasCome(GameObject newPlayer) {

        //Warn players themselves their roles have changed
        player.GetComponent<PlayerController>().SetMainPlayer(false);
        newPlayer.GetComponent<PlayerController>().SetMainPlayer(true);

        GameObject[] allOpponents = GameObject.FindGameObjectsWithTag("Opponent");
        foreach (GameObject opponent in allOpponents) {
            opponent.GetComponent<OpponentControllerAttack>().UpdatePlayer(newPlayer);
        }
        GameObject goal = GameObject.FindGameObjectWithTag("OpponentGoalKeeper");
        if (goal != null)
            goal.GetComponent<OpponentControllerGoal>().UpdatePlayer(newPlayer);

        GameObject staminaHUD = GameObject.FindGameObjectWithTag("StaminaHUD");
        if (staminaHUD != null)
            staminaHUD.GetComponent<UpdateStaminaSlider>().UpdatePlayer(newPlayer);

        ball.GetComponent<BallController>().UpdatePlayer(newPlayer);
        ball.GetComponent<BallMagnetism>().UpdatePlayer(newPlayer);
    }

    private void AssignObjectsToPlayer(GameObject newPlayer) {
        newPlayer.GetComponent<PlayerController>().SetBall(ball);
        newPlayer.GetComponent<PlayerController>().SetCam(cam);
        foreach (Transform child in newPlayer.transform) {
            if (child.GetComponent<PlayerShowBall>() != null)
                child.GetComponent<PlayerShowBall>().SetBall(ball);
        }
    }

    public void SetScoringPoints(float points) {
        scoringPoints = points;
    }

    public float GetScoringPoints() {
        return scoringPoints;
    }

    public GameObject GetAlly() {

        return ally;
    }

    public bool IsAllyAvailable() {

        return allyNum < numPassMax;
    }
}
