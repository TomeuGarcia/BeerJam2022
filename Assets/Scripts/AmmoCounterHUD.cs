using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounterHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCounter0;
    [SerializeField] TextMeshProUGUI ammoCounter1;

    [SerializeField] CanvasGroup tutorialCanvasGroup;


    private void Awake()
    {
        StartCoroutine(HideTutorial());
    }


    private void OnEnable()
    {
        PickUpCollector.OnPickUp += UpdateAmmoCounter;
    }

    private void OnDisable()
    {
        PickUpCollector.OnPickUp -= UpdateAmmoCounter;
    }


    private void UpdateAmmoCounter(int playerId, int ammo)
    {
        if (playerId == 0)
        {
            ammoCounter0.text = ammo.ToString();
        }
        else if (playerId == 1)
        {
            ammoCounter1.text = ammo.ToString();
        }
    }



    IEnumerator HideTutorial()
    {
        yield return new WaitForSeconds(30f);

        while (tutorialCanvasGroup.alpha > 0f)
        {
            tutorialCanvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
    }


}
