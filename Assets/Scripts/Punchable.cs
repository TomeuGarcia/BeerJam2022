using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Punchable : MonoBehaviour
{

    public UnityEvent OnPunch;

    public void Punch()
    {
        OnPunch.Invoke();
        
    }
}
