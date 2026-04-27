using UnityEngine;

[System.Serializable]
public class ScoreData 
{
    public float enemiesKilled = 0;
    public float damageDealt = 0;
    public float damageReceived = 0;
    public float doubloonsEarned = 0;
    public float doubloonsSpent = 0;
    public float time = 0;
    public float wavesCompleted = 0;

    public ScoreData(GameManager manager)
    {
        enemiesKilled = manager.enemiesKilled.GetValue();
        damageDealt = manager.damageDealt.GetValue();
        damageReceived = manager.damageReceived.GetValue();
        doubloonsEarned = manager.doubloonsEarned.GetValue();
        doubloonsSpent = manager.doubloonsSpent.GetValue();
        time = manager.time.GetValue();
        wavesCompleted = manager.wavesCompleted.GetValue();
    }

    public ScoreData()
    {

    }
}
