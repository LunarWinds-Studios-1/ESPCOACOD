using System;
using UnityEngine;

public class StatBoard : MonoBehaviour
{
    [SerializeField] StatDisplay enemiesKilledDisplay;
    [SerializeField] StatDisplay damageDealtDisplay;
    [SerializeField] StatDisplay damageRecievedDisplay;
    [SerializeField] StatDisplay doubloonsEarnedDisplay;
    [SerializeField] StatDisplay doubloonsSpentDisplay;
    [SerializeField] StatDisplay timeDisplay;
    [SerializeField] StatDisplay wavesCompletedDisplay;

    GameManager manager;
    ScoreData highscores;
    ScoreData oldHighScores;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindFirstObjectByType<GameManager>();
        oldHighScores = HighScoreSaveSystem.LoadHighScoreData();
        manager.SaveHighScore();
        highscores = HighScoreSaveSystem.LoadHighScoreData();

        UpdateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBoard()
    {
        enemiesKilledDisplay.SetIsHighScore(manager.enemiesKilled.GetValue(), oldHighScores.enemiesKilled);
        enemiesKilledDisplay.UpdateDisplay(manager.enemiesKilled.GetText(), ((int)manager.enemiesKilled.GetValue()).ToString(), ((int)highscores.enemiesKilled).ToString());
        damageDealtDisplay.SetIsHighScore(manager.damageDealt.GetValue(), oldHighScores.damageDealt);
        damageDealtDisplay.UpdateDisplay(manager.damageDealt.GetText(), ((int)manager.damageDealt.GetValue()).ToString(), ((int)highscores.damageDealt).ToString());
        damageRecievedDisplay.SetIsHighScore(manager.damageReceived.GetValue(), oldHighScores.damageReceived);
        damageRecievedDisplay.UpdateDisplay(manager.damageReceived.GetText(), ((int)manager.damageReceived.GetValue()).ToString(), ((int)highscores.damageReceived).ToString());
        doubloonsEarnedDisplay.SetIsHighScore(manager.doubloonsEarned.GetValue(), oldHighScores.doubloonsEarned);
        doubloonsEarnedDisplay.UpdateDisplay(manager.doubloonsEarned.GetText(), ((int)manager.doubloonsEarned.GetValue()).ToString(), ((int)highscores.doubloonsEarned).ToString());
        doubloonsSpentDisplay.SetIsHighScore(manager.doubloonsSpent.GetValue(), oldHighScores.doubloonsSpent);
        doubloonsSpentDisplay.UpdateDisplay(manager.doubloonsSpent.GetText(), ((int)manager.doubloonsSpent.GetValue()).ToString(), ((int)highscores.doubloonsSpent).ToString());
        timeDisplay.SetIsHighScore(manager.time.GetValue(), oldHighScores.time);
        timeDisplay.UpdateDisplay(manager.time.GetText(), TimeSpan.FromSeconds(manager.time.GetValue()).ToString(@"mm\:ss"), TimeSpan.FromSeconds(highscores.time).ToString(@"mm\:ss"));
        wavesCompletedDisplay.SetIsHighScore(manager.wavesCompleted.GetValue(), oldHighScores.wavesCompleted);
        wavesCompletedDisplay.UpdateDisplay(manager.wavesCompleted.GetText(), ((int)manager.wavesCompleted.GetValue()).ToString(), ((int)highscores.wavesCompleted).ToString());
        
    }
}
