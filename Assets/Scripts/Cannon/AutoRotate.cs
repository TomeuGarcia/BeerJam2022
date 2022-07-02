using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AutoRotate : MonoBehaviour
{
    [SerializeField] Transform rotatingTransform;
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    [SerializeField] float duration;


    private void OnEnable()
    {
        RotateToMax();
    }



    private void RotateToMax()
    {
        rotatingTransform.DORotate(Vector3.forward * maxRotation, duration).OnComplete(()=>RotateToMin()).SetEase(Ease.Linear);
    }

    private void RotateToMin()
    {
        rotatingTransform.DORotate(Vector3.forward * minRotation, duration).OnComplete(()=>RotateToMax()).SetEase(Ease.Linear);
    }


    public void Pause()
    {
        rotatingTransform.DOPause();
    }

    public void Resume()
    {
        rotatingTransform.DOPlay();
    }

    public void GoToStart()
    {
        rotatingTransform.DOKill();
        //rotatingTransform.DORotate(Vector3.forward * (rotatingTransform.eulerAngles.z - minRotation), 0.2f).SetEase(Ease.Linear);
    }


}
