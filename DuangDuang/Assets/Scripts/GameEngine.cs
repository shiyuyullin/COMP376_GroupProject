using System.Collections;
using System.Collections.Generic;
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
    //item effect time
    float effectsTime = 5f;
    float rotateSpeed = 2f;

    

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject enemyBot1 = Instantiate(enemyBots, new Vector3(13.16f, 0.0f, 4.935f),Quaternion.identity);
        GameObject enemyBot2 = Instantiate(enemyBots, new Vector3(18.1f, 0.0f, -11.1f), Quaternion.identity);
        GameObject enemyBot3 = Instantiate(enemyBots, new Vector3(18.1f, 0.0f, 23.1f), Quaternion.identity);
        GameObject friendlyBot1 = Instantiate(friendlyBots, new Vector3(-18.7f, 0.0f, -10), Quaternion.identity);
        GameObject friendlyBot2 = Instantiate(friendlyBots, new Vector3(-18.7f, 0.0f, 28.7f), Quaternion.identity);
        Time.timeScale = 0;

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
        GameObject iceCream = Instantiate(itemPrefab, position, itemPrefab.transform.rotation);
        //TODO rotate slowly
        Debug.Log("Item spawn!");
    }
}
