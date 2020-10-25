using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public static int scoreValue;
    public Text score;

    void Start()
    {
        scoreValue = 0;
        score.text = "SCORE: " + scoreValue;
    }

    void Update()
    {
        score.text = "SCORE: " + scoreValue;
    }
}
