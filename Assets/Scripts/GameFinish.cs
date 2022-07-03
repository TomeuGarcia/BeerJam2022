using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameFinish : MonoBehaviour
{
    [SerializeField] Transform bottleTransform;
    [SerializeField] GameObject endGameHUD;

    //private void Awake()
    //{
    //    LoadEndScene();
    //}

    private void OnEnable()
    {
        CovidWall.OnDeathEnd += PlayBottleAnimation;
    }

    private void OnDisable()
    {
        CovidWall.OnDeathEnd -= PlayBottleAnimation;
    }


    private void PlayBottleAnimation()
    {
        bottleTransform.DOPunchScale(new Vector3(0.3f, 0.2f, 0f), 2f, 5).OnComplete(() => LoadEndScene());
    }


    private void LoadEndScene()
    {
        Instantiate(endGameHUD);
    }


}
