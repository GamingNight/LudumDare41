﻿using System.Collections;
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
    public float ScoringPoints = 0;
    public float NumPassMax;

    private GameObject ally;
    private float horizontal;
    private float vertical;
    private int allyNum;
    private Vector3 pos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        ShareANewPlayerHasCome(player);
        GiveGameobjectsToNewPlayer();
        allyNum = 0;
    }
    void Start()
    {
        //GiveGameobjectsToNewPlayer();
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
        if (ally == null && allyNum < NumPassMax)
        {
            if (player.transform.position.y < -3)
            {
                pos = new Vector3(-1, 1, 0);
            }
            else if (player.transform.position.y > 3.6)
            {
                pos = new Vector3(-1, -1, 0);
            }
            else
            {
                if (Random.value >= 0.5)
                {
                    pos = new Vector3(-1, 1, 0);
                }
                else
                {
                    pos = new Vector3(-1, -1, 0);
                }
            }
            cam.GetComponent<CameraZoom>().size = 1.4f;
            ally = Instantiate<GameObject>(prefabPlayer, player.transform.position + pos, Quaternion.identity);
            foreach (Transform child in player.transform)
            {
                if (child.tag == "Clouds")
                {
                    foreach (Transform childSZ in child.transform)
                    {
                        if (childSZ.tag == "SafeZoneAlly3")
                        {
                            childSZ.gameObject.SetActive(true);
                            childSZ.gameObject.GetComponent<FollowTheAlly>().ally = ally;
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
                            childSZ.gameObject.GetComponent<FollowTheAlly>().ally = ally;
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
            ally.GetComponent<PlayerController>().ball = ball;
            ally.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0f);
            ally.GetComponent<PlayerController>().enabled = false;
            ally.GetComponent<AllySpeed>().enabled = true;
            foreach (Transform child in ally.transform)
            {
                if (child.gameObject.tag == "Clouds")
                    child.gameObject.SetActive(false);
                if (child.gameObject.tag == "PlayerCollider")
                    child.gameObject.SetActive(true);
            }
            allyNum = allyNum + 1;
        }
    }

    public void SetAllyAsNewPlayer()
    {
        if (ally != null)
        {
            ally.GetComponent<AllySpeed>().enabled = false; // le receveur n'accélère plus tout seul
            ally.GetComponent<PlayerController>().enabled = true; // le receveur est le nouveau joueur
            foreach (Transform child in player.transform)
            {
                if (child.tag == "Clouds")
                {
                    child.gameObject.SetActive(false); //désactivation des nuages autour de l'ex player
                }
            }
            foreach (Transform child in ally.transform)
            {
                if (child.tag == "Clouds")
                {
                    child.gameObject.SetActive(true); //désactivation des nuages autour de l'ex player
                }
            }
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
        //player.GetComponent<PlayerController>().goalKeeper = goalKeeper;
        player.GetComponent<PlayerController>().cam = cam;
        player.GetComponent<PlayerController>().ball = ball;
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
