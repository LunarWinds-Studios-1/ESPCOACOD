using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider lerpSlider;
    [SerializeField] Slider overhealSlider;

    public float maxHealth;
    [SerializeField] float lerpSpeed = 0.05f;

    [SerializeField] bool billboard = false;

    public void Initialize(float maxHealth)
    {
        this.maxHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        lerpSlider.maxValue = maxHealth;
        overhealSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        lerpSlider.value = maxHealth;
        overhealSlider.value = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != lerpSlider.value)
        {
            lerpSlider.value = Mathf.Lerp(lerpSlider.value, healthSlider.value, lerpSpeed);
        }

        if (billboard) { Billboard(); }
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        overhealSlider.value = health - healthSlider.value;
    }

    public void Billboard()
    {
        Vector3 targetPos = Camera.main.transform.position;

        transform.LookAt(targetPos);
    }
}
