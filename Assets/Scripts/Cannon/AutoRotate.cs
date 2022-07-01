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


    private void OnValidate()
    {
        if (minRotation > maxRotation) minRotation = maxRotation;
    }

    private void OnEnable()
    {
        RotateToMax();
    }

    private void OnDisable()
    {
        
    }

    public void Shoot()
    {

    }





    private void RotateToMax()
    {
        rotatingTransform.DORotate(Vector3.forward * maxRotation, duration).OnComplete(()=>RotateToMin()).SetEase(Ease.Linear);
    }

    private void RotateToMin()
    {
        rotatingTransform.DORotate(Vector3.forward * minRotation, duration).OnComplete(()=>RotateToMax()).SetEase(Ease.Linear);
    }



}
