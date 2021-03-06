using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleObject : IActivateable
{
    [SerializeField] bool startsEnabled = true;
    bool isEnabled = false;

    public float consumeTime = 5;
    public bool consumed = false; 
    void Start()
    {
        
    }

    public void Consume()
    {
        StartCoroutine("Consumer");
    }
    // Update is called once per frame

    IEnumerator Consumer()
    {
        consumed = true;
        yield return new WaitForSeconds(consumeTime);
        consumed = false;
    }
    void Update()
    {
        if (!startsEnabled)
        {
            if (!isEnabled) consumed = true;
        }
    }


    public override void Activate()
    {
        isEnabled = true;
        consumed = false;
    }
}
