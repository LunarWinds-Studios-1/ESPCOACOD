using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExplosionDecal : MonoBehaviour
{
    [SerializeField] DecalProjector projector;
    [SerializeField] float fadeTime = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float t = 0;
        while (t < fadeTime)
        {
            projector.fadeFactor = 1 - (t/ fadeTime);
            t+= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
