using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishHUD : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 0f;

        StartCoroutine(FadeIn());   
    }


    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene(1);
    }

}
