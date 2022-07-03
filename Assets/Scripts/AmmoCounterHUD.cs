using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounterHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCounter0;
    [SerializeField] TextMeshProUGUI ammoCounter1;

    [SerializeField] CanvasGroup tutorialCanvasGroup;
    [SerializeField] CanvasGroup tutorialCanonCanvasGroup;


    [SerializeField] CanvasGroup tutorialHookCanvasGroup;
    [SerializeField] CanvasGroup tutorialPunchCanvasGroup;

    int numPlayersMounted = 0;


    private void Awake()
    {
        StartCoroutine(HideTutorial());
        tutorialCanonCanvasGroup.alpha = 0f;

        tutorialHookCanvasGroup.alpha = 0f;
        tutorialPunchCanvasGroup.alpha = 0f;
    }


    private void OnEnable()
    {
        PickUpCollector.OnPickUp += UpdateAmmoCounter;
        CannonUse.OnPlayerMount += PlayerMountsCanon;
        CannonUse.OnPlayerDismount += PlayerDismountCanon;
        TutorialAbilityTrigger.OnPlayerEnter += DisplayTutorialAbility;
    }

    private void OnDisable()
    {
        PickUpCollector.OnPickUp -= UpdateAmmoCounter;
        CannonUse.OnPlayerMount -= PlayerMountsCanon;
        CannonUse.OnPlayerDismount -= PlayerDismountCanon;
        TutorialAbilityTrigger.OnPlayerEnter -= DisplayTutorialAbility;
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
        yield return new WaitForSeconds(20f);

        while (tutorialCanvasGroup.alpha > 0f)
        {
            tutorialCanvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
    }



    private void PlayerDismountCanon()
    {
        --numPlayersMounted;

        if (numPlayersMounted == 0)
        {
            StopCoroutine("CanonTutorialDisplay");
            tutorialCanonCanvasGroup.alpha = 0f;
        }
    }

    private void PlayerMountsCanon()
    {
        ++numPlayersMounted;
           
        if (numPlayersMounted == 1)
        {
            StartCoroutine("CanonTutorialDisplay");
        }

    }


    IEnumerator CanonTutorialDisplay()
    {
        tutorialCanonCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(30f);

        tutorialCanonCanvasGroup.alpha = 0f;
    }



    private void DisplayTutorialAbility(int playerId)
    {
        if (playerId == 0)
        {
            StartCoroutine(DisplayHookAbility());
        }
        else if (playerId == 1)
        {
            StartCoroutine(DisplayPunchAbility());
        }
    }


    IEnumerator DisplayHookAbility()
    {
        tutorialHookCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(15f);

        tutorialHookCanvasGroup.alpha = 0f;
    }

    IEnumerator DisplayPunchAbility()
    {
        tutorialPunchCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(15f);

        tutorialPunchCanvasGroup.alpha = 0f;
    }


}
