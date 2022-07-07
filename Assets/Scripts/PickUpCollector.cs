using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCollector : MonoBehaviour
{
    [SerializeField] Transform displayerReference;
    [SerializeField] AmmoPickUpDisplay ammoPickUpDisplayer;
    [SerializeField] int playerId = 0;
    public int pickUpCounter { get; private set; }

    public delegate void PickUpCollectorAction(int playerId, int ammoAmount);
    public static event PickUpCollectorAction OnPickUp;



    private void Awake()
    {
        pickUpCounter = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PickUp"))
        {
            int amount = collision.gameObject.GetComponent<PickUp>().Collect();
            Instantiate(ammoPickUpDisplayer).StartAnimation(amount, displayerReference.position);
            pickUpCounter += amount;

            if (OnPickUp != null) OnPickUp(playerId, pickUpCounter);
        }
    }





}
