using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public static PlayerManager GetInstance()
    {
        return instance;
    }

    public GameObject goalKeeper;
    public GameObject player;
    public Camera cam;
    public GameObject ball;
    public GameObject prefabPlayer;
    public float ballSpeed;
    public float ScoringPoints = 0;

    private GameObject ally;
    private float horizontal;
    private float vertical;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        ShareANewPlayerHasCome(player);
        GiveGameobjectsToNewPlayer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            InstantiateAlly();
        }
    }

    public void InstantiateAlly()
    {
        if (ally == null)
        {
            Vector3 pos = new Vector3(-1, 1, 0);
            cam.GetComponent<CameraZoom>().size = 1.4f;
            ally = Instantiate<GameObject>(prefabPlayer, player.transform.position + pos, Quaternion.identity);
            ally.GetComponent<PlayerController>().ball = ball;
            ally.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0f);
        }
    }

    public void SetAllyAsNewPlayer()
    {
        if (ally != null)
        {
            ally.GetComponent<AllySpeed>().enabled = false; // le receveur n'accélère plus tout seul
            ally.GetComponent<PlayerController>().enabled = true; // le receveur est le nouveau joueur
            cam.GetComponent<CameraZoom>().size = 0.72f;
            ShareANewPlayerHasCome(ally);
            ally = null;
        }
        else
        {
            Debug.Log("Fin du Game, lol une passe sans allié");
        }
    }

    public void GiveGameobjectsToNewPlayer()
    {
        if (ally != null)
        {
            player.GetComponent<PlayerController>().goalKeeper = goalKeeper;
        }
        else
        {
            Debug.Log("Fin du Game, lol une passe sans allié");
        }
    }

    private void ShareANewPlayerHasCome(GameObject newPlayer)
    {
        player = newPlayer;
        GameObject[] allOpponents = GameObject.FindGameObjectsWithTag("Opponent");
        foreach (GameObject opponent in allOpponents)
        {
            opponent.GetComponent<OpponentControllerAttack>().UpdatePlayer(newPlayer);
        }
        ball.GetComponent<BallController>().UpdatePlayer(newPlayer);
        ball.GetComponent<BallMagnetism>().UpdatePlayer(newPlayer);
        cam.GetComponent<CameraFollow>().target = newPlayer.transform;
    }
}
