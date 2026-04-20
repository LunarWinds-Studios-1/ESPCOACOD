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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FreezeEnemyPositions()
    {
        freezeEnemies.Invoke(this, EventArgs.Empty);
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
}
