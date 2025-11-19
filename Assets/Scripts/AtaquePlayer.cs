using System;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int daño;
    private float cooldown;
    private bool isCooldownEnabled;
    private Enemy enemigo;
    

    private void Start()
    {
        cooldown = 0;
        isCooldownEnabled = true;
        enemigo = GetComponent<Enemy>();

    }

    public void PlayerAtaqueDebil()
    {
        if (isCooldownEnabled)
        {
            enemigo.VidaEnemy = -1;
            cooldown = 0.2f;
            isCooldownEnabled = false;
        }
    }
    
    public void PlayerAtaqueFuerte()
    {
        if (isCooldownEnabled)
        {
            enemigo.VidaEnemy = -6;
            cooldown = 1f;
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

    private void Cooldown()
    {
        //cuando se le llame toma el valor del cooldown actual y va restando 0.1 cada 0.1 segundos
        //ver de hacer el timer con corrutina
        //las corruinas solo no se deben usar en cosas de pausar el juegp y en barras de progreso(algo que se actualice constantemente
        
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            isCooldownEnabled = true;
        }

    }
}
