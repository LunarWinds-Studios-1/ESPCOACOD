using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

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

    [Header("Wave Spawning")]
    List<Fish> enemyWave = new List<Fish>();
    [SerializeField] List<EnemySpawner> spawners = new List<EnemySpawner>();
    [SerializeField] List<SpawnableEnemy> spawnableEnemies = new List<SpawnableEnemy>();
    [SerializeField] int startingTokens = 10;
    [SerializeField] int spawnTokens = 10;
    [SerializeField] int entitySpawnCap = 50;
    [SerializeField] float fishSpawnRate = 5;

    [SerializeField] bool spawnAvailable = false;

    List<GameObject> fish = new List<GameObject>();

    [SerializeField] bool devMode = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTokens = startingTokens;
    }

    // Update is called once per frame
    void Update()
    {
        if (combatActive)
        {
            time.IncreaseStat(Time.deltaTime);
            if (enemyWave.Count == 0)
            {
                spawnAvailable = true;
                wavesCompleted.IncreaseStat(1);
            }
        }

        if (spawnAvailable && combatActive)
        {
            wave++;
            spawnAvailable = false;
            NewWave();
        }

        

        if (devMode)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                for (int i = enemyWave.Count - 1; i >= 0; i--)
                {
                    Debug.Log(enemyWave[i].gameObject.name);
                    enemyWave[i].Damage(10000000);
                }
            }
        }
    }

    public void DespawnEnemy(Fish fish)
    {
        if (enemyWave.Contains(fish))
        {
            enemyWave.Remove(fish);
        }
    }

    public void NewWave()
    {
        globalDifficulty = 1 + (Mathf.Pow(wave, 1.2f) / 10);
        spawnTokens = (int) ((float) startingTokens * globalDifficulty);
        SpawnWave();
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

    public List<SpawnableEnemy> GetAvailableEnemies(int budget)
    {
        List<SpawnableEnemy> availableEnemies = new List<SpawnableEnemy>();
        foreach (SpawnableEnemy enemy in spawnableEnemies)
        {
            if (enemy.minumumSpawningWave <= wave && enemy.tokenCost <= budget)
            {
                availableEnemies.Add(enemy);
            }
        }
        return availableEnemies;
    }

    public int GetMinimumTokenCost(List<SpawnableEnemy> availableEnemies)
    {
        int min = availableEnemies[0].tokenCost;
        foreach (SpawnableEnemy enemy in availableEnemies)
        {
            if (enemy.tokenCost < min)
            {
                min = enemy.tokenCost;
            }
        }
        return min;
    }

    public int GetMaximumTokenCost(List<SpawnableEnemy> availableEnemies)
    {
        int max = availableEnemies[0].tokenCost;
        foreach (SpawnableEnemy enemy in availableEnemies)
        {
            if (enemy.tokenCost > max)
            {
                max = enemy.tokenCost;
            }
        }
        return max;
    }

    public List<SpawnableEnemy> ShuffleEnemyList(List<SpawnableEnemy> list)
    {
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }

        return list;
    }

    public void SpawnWave()
    {
        int tokenBudget = spawnTokens;
        List<SpawnableEnemy> availableEnemies = GetAvailableEnemies(tokenBudget);
        int maxCost = GetMaximumTokenCost(availableEnemies);
        int minCost = GetMinimumTokenCost(availableEnemies);
        Queue enemySpawnQueue = new Queue();
        Debug.Log((tokenBudget >= minCost) +","+ (fish.Count < entitySpawnCap) +","+ (availableEnemies.Count > 0));
        while (tokenBudget >= minCost && enemySpawnQueue.Count < entitySpawnCap && availableEnemies.Count > 0)
        {
            availableEnemies = GetAvailableEnemies(tokenBudget);
            int weight = Mathf.Clamp(UnityEngine.Random.Range(0, GetMaximumTokenCost(availableEnemies)), 0, tokenBudget);
            availableEnemies = ShuffleEnemyList(availableEnemies);
            for (int i = 0; i < availableEnemies.Count; i++)
            {
                if (weight <= availableEnemies[i].tokenCost)
                {
                    enemySpawnQueue.Enqueue(availableEnemies[i].enemyPrefab);
                    tokenBudget -= availableEnemies[i].tokenCost;
                    break;
                }
            }
        }

        StartCoroutine(SpawnEnemyQueue(enemySpawnQueue));
    }

    public IEnumerator SpawnEnemyQueue(Queue enemies)
    {
        while (enemies.Count > 0)
        {
            GameObject fish = Instantiate(enemies.Dequeue() as GameObject, spawners[UnityEngine.Random.Range(0, spawners.Count)].transform.position, Quaternion.identity);
            enemyWave.Add(fish.GetComponent<Fish>());
            yield return new WaitForSeconds(1 / fishSpawnRate);
        }
    }
}
