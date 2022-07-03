using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCollector : MonoBehaviour
{
    public int pickUpCounter { get; private set; }


    private void Awake()
    {
        pickUpCounter = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PickUp"))
        {
            pickUpCounter += collision.gameObject.GetComponent<PickUp>().Collect();
        }
    }

}
