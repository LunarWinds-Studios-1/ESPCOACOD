using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenTransition : MonoBehaviour
{

    [SerializeField] Image flashImage;

    [SerializeField] Color flashColor;
    [SerializeField] Color restartFlashColor;

    [SerializeField] Color waterFogColor;
    [SerializeField] Color VoidFogColor;
    [SerializeField] float transitionTime;

    [Header("Cameras")]
    [SerializeField] Camera mainCamera;

    [SerializeField] LayerMask deathMask;
    [SerializeField] LayerMask normalMask;

    [SerializeField] Light sun;

    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RenderSettings.fogEndDistance = 35;

        restartFlashColor = Color.white;
        gameManager = FindFirstObjectByType<GameManager>();
        flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
        mainCamera.cullingMask = normalMask;
    }

    public void TransitionToDeathScreen()
    {
        StartCoroutine(CameraSwap());
    }

    public void TriggerLevelRestart()
    {
        flashImage.color = restartFlashColor;
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        float time = 0;

        while (time < transitionTime / 2)
        {
            time += Time.deltaTime;
            flashImage.color = new Color(restartFlashColor.r, restartFlashColor.g, restartFlashColor.b, time / (transitionTime / 2));
            yield return null;
        }
        flashImage.color = restartFlashColor;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator CameraSwap()
    {
        gameManager.DistortMusic();
        float time = 0;
        while (time < transitionTime / 2)
        {
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, time / (transitionTime / 2));
            yield return null;
            time += Time.unscaledDeltaTime;
        }
        mainCamera.cullingMask = deathMask;

        time = 0;
        RenderSettings.fogEndDistance = 20;
        RenderSettings.fogColor = Color.black;
        sun.transform.eulerAngles = new Vector3(-90, 0 , 0);
        Debug.Log(RenderSettings.fogEndDistance);
        gameManager.arena.SetActive(false);
        gameManager.FreezeEnemyPositions();
        while (time < transitionTime / 2)
        {
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 1 - time / (transitionTime / 2));
            yield return null;
            time += Time.unscaledDeltaTime;
        }

        flashImage.color = Color.clear;
    }
}
