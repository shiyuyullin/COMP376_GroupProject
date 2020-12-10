using UnityEngine;
using UnityEngine.UI;

public class FinalScorePanel : MonoBehaviour
{
    public static int finalScoreValue;
    public Text finalScore;

    void Start()
    {
        finalScoreValue = ScorePanel.scoreValue;
    }

    void Update()
    {
        finalScore.text = "SCORE: " + finalScoreValue;
    }
}
