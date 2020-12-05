using UnityEngine;
using UnityEngine.SceneManagement;

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
	if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene(0);
        }
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
