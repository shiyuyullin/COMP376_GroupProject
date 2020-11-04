using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startGame;
    public GameObject endGame;

    void Start()
    {
        Invoke("DisappearStartText", 1f);
    }

    void Update()
    {

    }


    public void ShowEndText()
    {
        endGame.SetActive(true);
    }

    public void DisappearStartText()
    {
        startGame.SetActive(false);
    }

}
