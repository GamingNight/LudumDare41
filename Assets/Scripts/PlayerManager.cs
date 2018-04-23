using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject ally;
    private float horizontal;
    private float vertical;
    private int allyNum;
    private Vector3 pos;
    private float scoringPoints;

    private void Awake() {
        if (instance == null)
            instance = this;
        ShareANewPlayerHasCome(player);
        AssignObjectsToPlayer(player);
        allyNum = 0;
        scoringPoints = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            InstantiateAlly();
        }
    }

    public void InstantiateAlly() {

        if (ally != null || allyNum >= numPassMax) {
            return;
        }

        //Compute spawn position
        if (player.transform.position.y < -3) {
            pos = new Vector3(-1, 1, 0);
        } else if (player.transform.position.y > 3.6) {
            pos = new Vector3(-1, -1, 0);
        } else {
            if (Random.value >= 0.5) {
                pos = new Vector3(-1, 1, 0);
            } else {
                pos = new Vector3(-1, -1, 0);
            }
        }

        //Camera unzoom to encompass player + ally
        cam.GetComponent<CameraZoom>().size = 1.4f;

        //Instantiate
        ally = Instantiate<GameObject>(prefabPlayer, player.transform.position + pos, Quaternion.identity);

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

        //Give ally an initial velocity
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
                    child.gameObject.SetActive(true); //désactivation des nuages autour de l'ex player
                }
            }
            cam.GetComponent<CameraZoom>().size = 0.72f;
            ShareANewPlayerHasCome(ally);
            ally = null;
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

        ball.GetComponent<BallController>().UpdatePlayer(newPlayer);
        ball.GetComponent<BallMagnetism>().UpdatePlayer(newPlayer);
        cam.GetComponent<CameraFollow>().target = newPlayer.transform;

        //Update player variable of this class
        player = newPlayer;
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
}
