using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler freezeEnemies;
    public float distortionTime = 3;

    public AudioSource musicPlayer;
    public AudioSource voidSoundPlayer;

    public GameObject arena;


    public Stat enemiesKilled = new Stat(0, "Enemies Killed");
    public Stat damageDealt = new Stat(0, "Damage Dealt");
    public Stat damageReceived = new Stat(0, "Damage Received");
    public Stat doubloonsEarned = new Stat(0, "Doubloons Earned");
    public Stat doubloonsSpent = new Stat(0, "Doubloons Spent");
    public Stat time = new Stat(0, "Time");
    public Stat wavesCompleted = new Stat(0, "Waves Completed");

    public bool combatActive = true;

    public float globalDifficulty = 1;
    public int wave = 0;

    public int doubloons = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (combatActive)
        {
            time.IncreaseStat(Time.deltaTime);
        }
    }

    public void FreezeEnemyPositions()
    {
        freezeEnemies?.Invoke(this, EventArgs.Empty);
    }

    public void DistortMusic()
    {
        StartCoroutine(Distort());
        StartCoroutine(fadeInAbyss());
    }

    public IEnumerator Distort()
    {
        float time = 0;

        while (time < distortionTime)
        {
            musicPlayer.pitch = Mathf.Lerp(1, 0.1f, time / distortionTime);
            musicPlayer.volume = Mathf.Lerp(1, 0.1f, time / distortionTime);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator fadeInAbyss()
    {
        float time = 0;

        while (time < distortionTime)
        {
            voidSoundPlayer.volume = Mathf.Lerp(0, 1, time / distortionTime);
            time += Time.deltaTime;
            yield return null;
        }

    }

    public void SaveHighScore()
    {
        ScoreData data = new ScoreData(this);
        ScoreData highscores = HighScoreSaveSystem.LoadHighScoreData();

        if (data.enemiesKilled > highscores.enemiesKilled)
        {
            highscores.enemiesKilled = data.enemiesKilled;
        }
        if (data.damageDealt > highscores.damageDealt)
        {
            highscores.damageDealt = data.damageDealt;
        }
        if (data.damageReceived > highscores.damageReceived)
        {
            highscores.damageReceived = data.damageReceived;
        }
        if (data.doubloonsEarned > highscores.doubloonsEarned)
        {
            highscores.doubloonsEarned = data.doubloonsEarned;
        }
        if (data.doubloonsSpent > highscores.doubloonsSpent)
        {
            highscores.doubloonsSpent = data.doubloonsSpent;
        }
        if (data.time > highscores.time)
        {
            highscores.time = data.time;
        }
        if (data.wavesCompleted > highscores.wavesCompleted)
        {
            highscores.wavesCompleted = data.wavesCompleted;
        }

        HighScoreSaveSystem.SaveScores(highscores);
    }
}
