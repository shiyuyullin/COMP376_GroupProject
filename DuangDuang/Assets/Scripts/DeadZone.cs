using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class DeadZone : MonoBehaviour
{
    string sceneName;

    //Map-J
    GameObject[] respawns;
    int respawnCounter = 0;
    int counter = 0;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "Map-J")
        {
            respawns = GameObject.FindGameObjectsWithTag("Respawn");
        }
        if(sceneName == "Map-Sh")
        {
            respawns = GameObject.FindGameObjectsWithTag("Respawn");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "TeamA")
        {
            ScorePanel.scoreValue--;
            StartCoroutine(BackToGame(col.gameObject));
        }
        else if (col.gameObject.tag == "TeamB")
        {
            ScorePanel.scoreValue++;
            StartCoroutine(BackToGame(col.gameObject));
        }

    }

    public IEnumerator BackToGame(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        //default position
        obj.transform.position = new Vector3(-10, 0, -10);
        //Map-J position
        if (sceneName == "Map-J")
        {
            obj.transform.position = respawns[respawnCounter].transform.position;
            respawnCounter++;
            if(respawnCounter == respawns.Length - 1)
            {
                respawnCounter = 0;
            }
        }
        if(sceneName == "Map-Sh")
        {
            if(obj.tag == "TeamA")
            {
                obj.transform.position = respawns[0].transform.position;
            }
            if(obj.tag == "TeamB")
            {
                obj.transform.position = respawns[3].transform.position;

            }
        }
    }
}
