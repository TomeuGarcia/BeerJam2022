using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    [SerializeField] IActivateable[] activateables;

    public virtual void Trigger()
    {
        DoTrigger();
    }

    protected void DoTrigger()
    {
        TriggerAllActivteables();
    }


    private void TriggerAllActivteables()
    {
        for (int i = 0; i < activateables.Length; ++i)
        {
            activateables[i].Activate();
        }
    }


}
