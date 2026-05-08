using UnityEngine;

public class SeaMonsterSpawner : MonoBehaviour
{
    Player player;

    public float spawnDistance = 200;
    public GameObject monster;
    Serpent serpent;
    bool spawned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            if (Mathf.Abs(player.gameObject.transform.position.magnitude) > spawnDistance)
            {
                spawned = true;
                Vector3 spawnPosition = player.transform.position + (Camera.main.transform.forward - Camera.main.transform.right * 3) * 100;
                GameObject m = Instantiate(monster, spawnPosition, Quaternion.identity);
                m.transform.LookAt(-Camera.main.transform.position);
                serpent = FindFirstObjectByType<Serpent>();
                serpent.origin = player.transform.position;
                serpent.targetPosition = (Camera.main.transform.forward + Camera.main.transform.right) * 100;
                Debug.Break();
            }
        }
    }
}
