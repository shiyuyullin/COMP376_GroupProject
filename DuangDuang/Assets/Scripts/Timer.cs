using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float time;
    public Text timer;
    public float startTime;
    public bool timerRunning;
    public GameManager gameManager;

    void Start()
    {
        startTime = 10f;
        timerRunning = true;
        time = startTime;
        timer.text = "Time: " + time;
    }

    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            timer.text = "Time: " + Mathf.FloorToInt(time);

            //timer.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60));
        }
        else
        {
            time = 0f;
            timer.text = "Time: " + Mathf.FloorToInt(time);
            timerRunning = false;
            gameManager.ShowEndText();
            Invoke("GameEnd", 1f);
        }
    }

    void GameEnd()
    {
        SceneManager.LoadScene(2);
    }
}
