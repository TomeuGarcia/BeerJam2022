using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private SceneSwitch sceneSwitch;
    [SerializeField] private bool fadeOutOnAwake = false;


    private void Awake()
    {
        if (fadeOutOnAwake)
        {
            DoFadeOutToScene();
        }
    }

    public void DoFadeInToScene(int sceneindex) 
    {
        StartCoroutine(FadeIn(sceneindex));
    }
    public void DoFadeOutToScene()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn(int sceneindex)
    {
        Color color = image.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime;
            image.color = color;
            yield return null;

        }
        yield return new WaitForSeconds(0.5f);
        sceneSwitch.OpenScene(sceneindex);

    }


    IEnumerator FadeOut()
    {
        Color color = image.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            image.color = color;
            yield return null;

        }
    }

}
