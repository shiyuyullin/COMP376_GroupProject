using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startGame;
    public GameObject endGame;

    void Start()
    {
        Invoke("DisappearStartText", 1f);
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
