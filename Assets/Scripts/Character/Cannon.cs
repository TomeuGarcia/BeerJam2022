using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    int ammo = 0;
    int currentAmmo = 0;

    bool HasEnoughAmmo => currentAmmo > 0;


    bool cannonInUse = true;


    private void Update()
    {
        if (!cannonInUse) return;


    }



    public void AddAmmo(int amount)
    {
        ammo += amount;
    }


    private void ShootAction()
    {
        if (!HasEnoughAmmo)
        {
            return;
        }


        --currentAmmo;
    }






}
