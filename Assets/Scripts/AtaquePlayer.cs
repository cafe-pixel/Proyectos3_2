using System;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int daño;
    private float cooldown;
    private bool isCooldownEnabled;
    

    private void Start()
    {
        cooldown = 0;
        isCooldownEnabled = true;
        GetComponent<Enemie>();

    }

    public void PlayerAtaqueDebil()
    {
        if (isCooldownEnabled)
        {
            Enemie.Vida = -1;
            cooldown = 0.2f;
            isCooldownEnabled = false;
        }
    }
    
    public void PlayerAtaqueFuerte()
    {
        if (isCooldownEnabled)
        {
            Enemie.Vida = -6;
            cooldown = 0.2f;
            isCooldownEnabled = false;
        }

    }


    /* ATAQUE DEBIL:
     COOLDOWN 0.2 S
     DAÑO 1
     desactiva iscooldown y manda el cooldown timer
     */
    /*ATAQUE FUERTE:
     COOLDOWN 1S
     DAÑO 6
     */

    private bool Cooldown()
    {
        //cuando se le llame toma el valor del cooldown actual y va restando 0.1 cada 0.1 segundos
        
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            isCooldownEnabled = true;
        }

    }
}
