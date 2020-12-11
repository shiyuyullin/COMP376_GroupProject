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
    float itemTimer = 0;
    int totalItem = 3;
    int createdItem = 0;
    private GameObject fallPlat1;
    private GameObject fallPlat2;
    private float fallPlatRespawnTimer;
    private bool spawnFallPlat;

    GameObject[] respawns;
    GameObject[] itemRespawns;

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
        if(sceneName =="Map-L"){
            float temp;
            float x;
            float y;
            float z;
            for(int i =1; i<6;i++){
                temp=i/6.0f*2*Mathf.PI;
            
                x=22*Mathf.Cos(temp);
                z=22*Mathf.Sin(temp);
                y=1;
               
                if(i==1||i==2){
                    Instantiate(friendlyBots,new Vector3(x,y,z),Quaternion.identity);
                }
                else {
                    Instantiate(enemyBots,new Vector3(x,y,z),Quaternion.identity);
                }

            }
            
            

            
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

        if (sceneName == "Map-Zi")
        {
            //fallPlat1 = Instantiate(fallPlat, new Vector3(-42.34f, 0.858f, -12.856f), Quaternion.Euler(0, 42.793f, 0));
            //fallPlat2 = Instantiate(fallPlat, new Vector3(-44.45f, 0.858f, -10.92f), Quaternion.Euler(0, 42.793f, 0));
            //spawnFallPlat = true;
            shRespawnPositions = GameObject.FindGameObjectsWithTag("Respawn");
            GameObject enemyBot1 = Instantiate(enemyBots, shRespawnPositions[3].transform.position, Quaternion.identity);
            GameObject enemyBot2 = Instantiate(enemyBots, shRespawnPositions[4].transform.position, Quaternion.identity);
        }

        if (sceneName == "Map-YZ")
        {
            respawns = GameObject.FindGameObjectsWithTag("Respawn");
            GameObject enemyBot1 = Instantiate(enemyBots, respawns[0].transform.position, Quaternion.identity);
            GameObject enemyBot2 = Instantiate(enemyBots, respawns[2].transform.position, Quaternion.identity);
            GameObject enemyBot3 = Instantiate(enemyBots, respawns[4].transform.position, Quaternion.identity);
            GameObject friendlyBot1 = Instantiate(friendlyBots, respawns[1].transform.position, Quaternion.identity);
            GameObject friendlyBot2 = Instantiate(friendlyBots, respawns[3].transform.position, Quaternion.identity);
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
        itemTimer += Time.deltaTime;
        if(itemTimer >= 35.0f)
        {
            spawnItem();
            itemTimer = 0.0f;
        }
    

        //item appear
        
        if (createdItem < totalItem)
        
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

        if(sceneName =="Map-L"){

            int t = Random.Range(0,180);
            float x=7*Mathf.Cos(t);
            float z=5*Mathf.Sin(t);
            float y=0;
            position = new Vector3(x,y,z);
            Instantiate(itemPrefab, position, itemPrefab.transform.rotation);
            t= Random.Range(0,180);
            x=7*Mathf.Cos(t);
            z=5*Mathf.Sin(t);
            position = new Vector3(x,y,z);
            Instantiate(itemPrefab, position, itemPrefab.transform.rotation);

        }
        
        

        if (sceneName == "Map-YZ")
        {
            itemRespawns = GameObject.FindGameObjectsWithTag("itemPosition");
            
            GameObject iceCream1 = Instantiate(itemPrefab, itemRespawns[Random.Range(0, itemRespawns.Length)].transform.position, itemPrefab.transform.rotation);
            GameObject iceCream2 = Instantiate(itemPrefab, itemRespawns[Random.Range(0, itemRespawns.Length)].transform.position, itemPrefab.transform.rotation);
            GameObject iceCream3 = Instantiate(itemPrefab, itemRespawns[Random.Range(0, itemRespawns.Length)].transform.position, itemPrefab.transform.rotation);
        }
       //TODO rotate slowly
        Debug.Log("Item spawn!");

    }

    public void quitToStartMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    
}
