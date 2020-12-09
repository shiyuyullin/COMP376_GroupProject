using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameEngine : MonoBehaviour
{
    [SerializeField] private GameObject mainPlayer; // 1
    [SerializeField] private GameObject enemyBots; // 3
    [SerializeField] private GameObject friendlyBots; // 2
    [SerializeField] private GameObject pauseMenu;

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
    private bool pause;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
        pauseMenu.SetActive(false);
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

        if(sceneName == "Map-Sh")
        {
            fallPlat1 = Instantiate(fallPlat, new Vector3(-42.34f, 0.858f, -12.856f), Quaternion.Euler(0, 42.793f, 0));
            fallPlat2 = Instantiate(fallPlat, new Vector3(-44.45f, 0.858f, -10.92f), Quaternion.Euler(0, 42.793f, 0));
            spawnFallPlat = true;
            shRespawnPositions = GameObject.FindGameObjectsWithTag("Respawn");
            GameObject friendlyBot1 = Instantiate(friendlyBots, shRespawnPositions[0].transform.position, Quaternion.identity);
            //GameObject friendlyBot2 = Instantiate(friendlyBots, shRespawnPositions[5].transform.position, Quaternion.identity);
            //GameObject friendlyBot3 = Instantiate(friendlyBots, shRespawnPositions[6].transform.position, Quaternion.identity);
            GameObject enemyBot1 = Instantiate(enemyBots, shRespawnPositions[3].transform.position, Quaternion.identity);
            GameObject enemyBot2 = Instantiate(enemyBots, shRespawnPositions[4].transform.position, Quaternion.identity);
            //GameObject enemyBot3 = Instantiate(enemyBots, shRespawnPositions[3].transform.position, Quaternion.identity);
            //GameObject enemyBot4 = Instantiate(enemyBots, shRespawnPositions[4].transform.position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup >= 5)
        {
            if (!Input.GetKeyDown(KeyCode.Escape) && !pause)
            { 
                Time.timeScale = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !pause)
            {
                Time.timeScale = 0.0f;
                pauseMenu.SetActive(true);
                pause = true;

            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pause)
            {
                Time.timeScale = 1.0f;
                pauseMenu.SetActive(false);
                pause = false;
            }
        }

        //items will appear every 20 seconds, and will be avaliable for 15 seconds.
        timer += Time.deltaTime;
        if(timer >= 35.0f)
        {
            spawnItem();
            timer = 0.0f;
        }
        
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
            GameObject iceCream = Instantiate(itemPrefab, position, itemPrefab.transform.rotation);
        }
        if(sceneName == "Map-Sh")
        {
            Vector3 position1 = new Vector3(-46.88069f, 0.9100018f, -45.67363f);
            GameObject iceCream1 = Instantiate(itemPrefab, position1, itemPrefab.transform.rotation);
            //(-8.98f,1.025999f,-40.84062f)
            Vector3 position2 = new Vector3(-8.98f, 1.025999f, -40.84062f);
            GameObject iceCream2 = Instantiate(itemPrefab, position2, itemPrefab.transform.rotation);
            //(-47.61237f,1.19f,-7.983576f)
            Vector3 position3 = new Vector3(-47.61237f, 1.19f, -7.983576f);
            GameObject iceCream3 = Instantiate(itemPrefab, position3, itemPrefab.transform.rotation);
        }

    }

    public void quitToStartMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
