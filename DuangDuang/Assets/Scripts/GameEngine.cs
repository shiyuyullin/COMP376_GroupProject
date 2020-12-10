using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    [SerializeField] private GameObject mainPlayer; // 1
    [SerializeField] private GameObject enemyBots; // 3
    [SerializeField] private GameObject friendlyBots; // 2


    //private float timer;

    [SerializeField]
    GameObject itemPrefab;
    float spawnTime = 5f;
    float timer = 0;
    int totalItem = 1;
    int createdItem = 0;

    //Map-J respawns
    GameObject[] respawns;
    string sceneName;
    //Map-L 
    public GameObject teaCup;
    public GameObject [] teaCups = new GameObject[5];
    float angle;
    float r;
    float distance=15;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        sceneName = currentScene.name;
        Debug.Log(sceneName);
        
        if (sceneName == "Map-J")
        {
            respawns = GameObject.FindGameObjectsWithTag("Respawn");
            Debug.Log(respawns.Length);
            GameObject enemyBot1 = Instantiate(enemyBots, respawns[0].transform.position, Quaternion.identity);
            GameObject enemyBot2 = Instantiate(enemyBots, respawns[2].transform.position, Quaternion.identity);
            GameObject enemyBot3 = Instantiate(enemyBots, respawns[4].transform.position, Quaternion.identity);
            GameObject friendlyBot1 = Instantiate(friendlyBots, respawns[1].transform.position, Quaternion.identity);
            GameObject friendlyBot2 = Instantiate(friendlyBots, respawns[3].transform.position, Quaternion.identity);
        }
        if(sceneName =="Map-L"){
            

            r=distance;
            angle=0;
            for(int i =0 ; i<5; i++){
                float temp=i/5.0f *2*Mathf.PI;
                float x=distance*Mathf.Cos(temp);
                float z=distance*Mathf.Sin(temp);
                float y=0;

                teaCups[i]= Instantiate(teaCup,new Vector3(x,y,z),Quaternion.identity);
            }
        }

        //GameObject enemyBot1 = Instantiate(enemyBots, new Vector3(13.16f, 0.0f, 4.935f),Quaternion.identity);
        //GameObject enemyBot2 = Instantiate(enemyBots, new Vector3(18.1f, 0.0f, -11.1f), Quaternion.identity);
        //GameObject enemyBot3 = Instantiate(enemyBots, new Vector3(18.1f, 0.0f, 23.1f), Quaternion.identity);
        //GameObject friendlyBot1 = Instantiate(friendlyBots, new Vector3(-18.7f, 0.0f, -10), Quaternion.identity);
        //GameObject friendlyBot2 = Instantiate(friendlyBots, new Vector3(-18.7f, 0.0f, 28.7f), Quaternion.identity);
        Time.timeScale = 0;

    }


    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup >= 5)
        {
            Time.timeScale = 1;
        }
        timer += Time.deltaTime;
        if(sceneName=="Map-L"){
            moveTeaCup(timer);
        }

        //item appear
        
        if (createdItem < totalItem)
        {
            if (timer > spawnTime)
            {
                spawnItem();
                createdItem ++;
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
        if(sceneName =="Map-L"){
            position = new Vector3(0,0,0);
        }
        GameObject iceCream = Instantiate(itemPrefab, position, itemPrefab.transform.rotation);
        //TODO rotate slowly
        Debug.Log("Item spawn!");
    }
    void moveTeaCup(float timer){
        float temp=angle;
        angle=angle+Time.deltaTime/2.0f;
        if(angle > 2* Mathf.PI){
            angle-=2*Mathf.PI;
        }
        r=distance*(Mathf.Cos(timer/2.0f)+1)/2.0f+5;
        for(int i =0 ; i<5; i++){
            
            if(temp > 2* Mathf.PI){
                temp-=2*Mathf.PI;
            }
            
            float x=r*Mathf.Cos(temp);
            float z=r*Mathf.Sin(temp);
            float y=0;

            teaCups[i].transform.position=new Vector3(x,y,z);
            temp+=1/5.0f *2*Mathf.PI;;
            }

    }
}
