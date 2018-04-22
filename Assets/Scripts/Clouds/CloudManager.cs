using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    public GameObject player;
    public GameObject cloud;
    public CircleCollider2D safeZone;

    public float spawnRadius;
    public int cloudPerStep;
    public int maxClouds = 1000;
    public float spawnWait;

    private List<SpriteRenderer> cloudRenderers;
    private float bufferTotalTime;
    private float bufferTime;

    void Start()
    {
        bufferTotalTime = 1;
        bufferTime = 0;
        cloudRenderers = new List<SpriteRenderer>();
        StartCoroutine(SpawnWaves());
        for (int i = 0; i < maxClouds; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(player.transform.position.x - spawnRadius, player.transform.position.x + spawnRadius), Random.Range(player.transform.position.y - spawnRadius, player.transform.position.y + spawnRadius), 0f);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject instantiateCloud = Instantiate(cloud, spawnPosition, spawnRotation);
            instantiateCloud.transform.SetParent(transform);
            instantiateCloud.GetComponent<Cloud>().safeZone = safeZone;
            cloudRenderers.Add(instantiateCloud.GetComponent<SpriteRenderer>());
        }
    }

    void Update()
    {
        bufferTime += Time.deltaTime;
        if (bufferTime < bufferTotalTime)
            return;
        List<SpriteRenderer> _cloudRenderers = new List<SpriteRenderer>(cloudRenderers);
        foreach (SpriteRenderer sprite in _cloudRenderers)
        {
            if ((sprite.transform.position - player.transform.position).magnitude > spawnRadius)
            {
                cloudRenderers.Remove(sprite);
                Destroy(sprite.gameObject);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            int i = 0;
            while (i < cloudPerStep && cloudRenderers.Count < maxClouds)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(player.transform.position.x - spawnRadius, player.transform.position.x + spawnRadius), Random.Range(player.transform.position.y - spawnRadius, player.transform.position.y + spawnRadius), 0f);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject instantiateCloud = Instantiate(cloud, spawnPosition, spawnRotation);
                instantiateCloud.transform.SetParent(transform);
                instantiateCloud.GetComponent<Cloud>().safeZone = safeZone;
                cloudRenderers.Add(instantiateCloud.GetComponent<SpriteRenderer>());
                i++;
            }
            yield return new WaitForSeconds(spawnWait);
        }
    }
}