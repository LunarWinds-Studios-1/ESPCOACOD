using UnityEngine;

public class HealthbarDebugger : MonoBehaviour
{
    public HealthBar hpBar;
    float health = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpBar.Initialize(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            health -= 10;
            hpBar.SetHealth(health);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            health += 10;
            hpBar.SetHealth(health);
        }
    }
}
