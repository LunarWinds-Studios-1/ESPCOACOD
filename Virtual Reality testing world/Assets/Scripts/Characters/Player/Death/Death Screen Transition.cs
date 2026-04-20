using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenTransition : MonoBehaviour
{

    [SerializeField] Image flashImage;
    [SerializeField] Image theInfiniteVoid;
    [SerializeField] Color flashColor;
    [SerializeField] Color restartFlashColor;
    [SerializeField] float transitionTime;

    [Header("Cameras")]
    [SerializeField] Camera mainCamera;

    [SerializeField] LayerMask deathMask;
    [SerializeField] LayerMask normalMask;

    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theInfiniteVoid.color = Color.white;
        restartFlashColor = Color.white;
        gameManager = FindFirstObjectByType<GameManager>();
        flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
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
        theInfiniteVoid.color = Color.black;
        time = 0;
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
