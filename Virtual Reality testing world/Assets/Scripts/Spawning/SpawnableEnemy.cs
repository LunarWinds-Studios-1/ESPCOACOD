using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableEnemy", menuName = "Scriptable Objects/SpawnableEnemy")]
public class SpawnableEnemy : ScriptableObject
{
    public GameObject enemyPrefab;
    public int tokenCost = 2;
    public int minumumSpawningWave = 1;
}
