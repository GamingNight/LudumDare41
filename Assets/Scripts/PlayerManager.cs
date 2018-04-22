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

    public GameObject player;
    public Camera cam;
    public GameObject ball;
    public GameObject prefabPlayer;
    public float ballSpeed;

    private GameObject ally;
    private float horizontal;
    private float vertical;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            InstantiateAlly();
        }
    }

    private void InstantiateAlly()
    {
        Vector3 pos = new Vector3(-1, 1, 0);
        ally = Instantiate<GameObject>(prefabPlayer, player.transform.position + pos, Quaternion.identity);
        ally.GetComponent<PlayerController>().ball = ball;
        ally.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0f);
    }

    public void SetAllyAsNewPlayer()
    {
        if (ally != null)
        {
            ally.GetComponent<AllySpeed>().enabled = false; // le receveur n'accélère plus tout seul
            ally.GetComponent<PlayerController>().enabled = true; // le receveur est le nouveau joueur
            ShareANewPlayerHasCome();
        }
        else
        {
            Debug.Log("Fin du Game, lol une passe sans allié");
        }
    }

    private void ShareANewPlayerHasCome()
    {
        player = ally;
        GameObject[] allOpponents = GameObject.FindGameObjectsWithTag("Opponent");
        foreach (GameObject opponent in allOpponents)
        {
            opponent.GetComponent<OpponentControllerAttack>().UpdatePlayer(ally);
        }
        ball.GetComponent<BallController>().UpdatePlayer(ally);
        ball.GetComponent<BallMagnetism>().UpdatePlayer(ally);
        cam.GetComponent<CameraFollow>().target = ally.transform;
    }
}
