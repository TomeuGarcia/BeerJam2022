using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] Transform parentTransform;
    [SerializeField] Transform floatingTransform;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float distance = 0.25f;
    float time;



    private void Awake()
    {
        time = Random.Range(0f, 2f);
    }

    private void Update()
    {
        floatingTransform.position = (Vector3.up * Mathf.Sin(time) * distance) + parentTransform.position;
        time += Time.deltaTime * speed;
    }

}
