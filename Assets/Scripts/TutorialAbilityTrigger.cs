using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAbilityTrigger : MonoBehaviour
{
    [SerializeField] int playerId;

    public delegate void TutorailAbilityAction(int playerId);
    public static event TutorailAbilityAction OnPlayerEnter;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (OnPlayerEnter != null) OnPlayerEnter(playerId);
            Destroy(gameObject);
        }
    }


}
