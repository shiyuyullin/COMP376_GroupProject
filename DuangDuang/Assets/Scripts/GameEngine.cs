﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    [SerializeField] private GameObject mainPlayer; // 1
    [SerializeField] private GameObject enemyBots; // 3
    [SerializeField] private GameObject friendlyBots; // 2


    //private float timer;
    [SerializeField] private GameObject fallPlat;
    [SerializeField] private GameObject itemPrefab;
    float spawnTime = 5f;
    float timer = 0;
    int totalItem = 3;
    int createdItem = 0;
    private GameObject fallPlat1;
    private GameObject fallPlat2;
    private float fallPlatRespawnTimer;
    private bool spawnFallPlat;
    //Map-J respawns
    GameObject[] respawns;
    //Map-Sh respawn positions
    GameObject[] shRespawnPositions;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        
        if (sceneName == "Map-J")
        {
            respawns = GameObject.FindGameObjectsWithTag("Respawn");
            GameObject enemyBot1 = Instantiate(enemyBots, respawns[0].transform.position, Quaternion.identity);
            GameObject enemyBot2 = Instantiate(enemyBots, respawns[2].transform.position, Quaternion.identity);
            GameObject enemyBot3 = Instantiate(enemyBots, respawns[4].transform.position, Quaternion.identity);
            GameObject friendlyBot1 = Instantiate(friendlyBots, respawns[1].transform.position, Quaternion.identity);
            GameObject friendlyBot2 = Instantiate(friendlyBots, respawns[3].transform.position, Quaternion.identity);
        }

        //GameObject enemyBot1 = Instantiate(enemyBots, new Vector3(13.16f, 0.0f, 4.935f),Quaternion.identity);
        //GameObject enemyBot2 = Instantiate(enemyBots, new Vector3(18.1f, 0.0f, -11.1f), Quaternion.identity);
        //GameObject enemyBot3 = Instantiate(enemyBots, new Vector3(18.1f, 0.0f, 23.1f), Quaternion.identity);
        //GameObject friendlyBot1 = Instantiate(friendlyBots, new Vector3(-18.7f, 0.0f, -10), Quaternion.identity);
        //GameObject friendlyBot2 = Instantiate(friendlyBots, new Vector3(-18.7f, 0.0f, 28.7f), Quaternion.identity);
        Time.timeScale = 0;
        if(sceneName == "Map-Sh")
        {
            fallPlat1 = Instantiate(fallPlat, new Vector3(-42.34f, 0.858f, -12.856f), Quaternion.Euler(0, 42.793f, 0));
            fallPlat2 = Instantiate(fallPlat, new Vector3(-44.45f, 0.858f, -10.92f), Quaternion.Euler(0, 42.793f, 0));
            spawnFallPlat = true;
            shRespawnPositions = GameObject.FindGameObjectsWithTag("Respawn");
            GameObject friendlyBot1 = Instantiate(friendlyBots, shRespawnPositions[0].transform.position, Quaternion.identity);
            GameObject friendlyBot2 = Instantiate(friendlyBots, shRespawnPositions[1].transform.position, Quaternion.identity);
            //GameObject friendlyBot3 = Instantiate(friendlyBots, shRespawnPositions[2].transform.position, Quaternion.identity);
            GameObject enemyBot1 = Instantiate(enemyBots, shRespawnPositions[3].transform.position, Quaternion.identity);
            GameObject enemyBot2 = Instantiate(enemyBots, shRespawnPositions[4].transform.position, Quaternion.identity);
            GameObject enemyBot3 = Instantiate(enemyBots, shRespawnPositions[5].transform.position, Quaternion.identity);
            //GameObject enemyBot4 = Instantiate(enemyBots, shRespawnPositions[6].transform.position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup >= 5)
        {
            Time.timeScale = 1;
        }

        //item appear
        timer += Time.deltaTime;
        if (createdItem <= totalItem)
        {
            if (timer >= spawnTime)
            {
                spawnItem();
                createdItem ++;
            }
        }
        //
        if (fallPlat1 == null && spawnFallPlat)
        {
            fallPlatRespawnTimer += Time.deltaTime;
            if (fallPlatRespawnTimer >= 1.5f)
            {
                fallPlat1 = Instantiate(fallPlat, new Vector3(-42.34f, 0.858f, -12.856f), Quaternion.Euler(0, 42.793f, 0));
                fallPlatRespawnTimer = 0.0f;

            }
        }
        if (fallPlat2 == null && spawnFallPlat)
        {
            fallPlatRespawnTimer += Time.deltaTime;
            if (fallPlatRespawnTimer >= 1.5f)
            {
                fallPlat2 = Instantiate(fallPlat, new Vector3(-44.45f, 0.858f, -10.92f), Quaternion.Euler(0, 42.793f, 0));
                fallPlatRespawnTimer = 0.0f;

            }
        }
    }

    void spawnItem()
    {
        Vector3 position = new Vector3(0, 0.1f, 25);
        if (sceneName == "Map-J")
        {
            position = new Vector3(0, 0, 0);
        }
        if(sceneName == "Map-Sh")
        {
            Vector3 position1 = new Vector3(-46.88069f, 0.9100018f, -45.67363f);
            //(-8.98f,1.025999f,-40.84062f)
            //(-47.61237f,1.19f,-7.983576f)
        }
        GameObject iceCream = Instantiate(itemPrefab, position, itemPrefab.transform.rotation);
    }
}
