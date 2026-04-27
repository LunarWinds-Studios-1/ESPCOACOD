using TMPro;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highScoreDisplay;
    public TextMeshProUGUI newHighScoreIndicator;
    public TextMeshProUGUI statName;

    bool isHighscore = false;

    private void Start()
    {
    }
    public void UpdateDisplay(string name, string score, string highscore = "0")
    {
        statName.text = name + ": ";   
        scoreDisplay.text = score;
        highScoreDisplay.text = highscore;
        Debug.Log(isHighscore);
        
    }

    public void SetIsHighScore(float score, float highscore)
    {
        newHighScoreIndicator.enabled = score > highscore;
    }
}
