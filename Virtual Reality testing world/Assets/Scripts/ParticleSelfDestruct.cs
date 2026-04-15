using UnityEngine;

public class ParticleSelfDestruct : MonoBehaviour
{
    ParticleSystem particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!particles.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
