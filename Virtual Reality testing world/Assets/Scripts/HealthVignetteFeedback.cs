using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class HealthVignetteFeedback : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] Player player;

    [SerializeField] Color damageColor;
    [SerializeField] Color healColor;
    Vignette vignette;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        volume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth < player.maxHealth / 2)
        {
            float intensity = 1 - (player.currentHealth / (player.maxHealth / 2));
            vignette.intensity.value = intensity;
            vignette.color.value = damageColor;
        } else if (player.currentHealth > player.maxHealth)
        {
            float intensity = ((player.currentHealth - player.maxHealth) / (player.maxHealth));
            vignette.intensity.value = intensity;
            vignette.color.value = healColor;   
        } else
        {
            vignette.intensity.value = 0;
        }
        
    }
}
