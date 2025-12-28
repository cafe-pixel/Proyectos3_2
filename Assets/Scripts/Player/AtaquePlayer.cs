using System;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int daño;
    private Player player;
    
    //states
    private string state = "idle";
    
    //ataque suave
    [SerializeField] private KeyCode attack = KeyCode.Q;
    [SerializeField] private float suaveMaxTimer;
    private float suaveTimer;
    private bool canGiveDamage1 = false;
    
    //ataque fuerte
    [SerializeField]
    
    

    private void Start()
    { 
        player = GetComponent<Player>();

    }

    private void Update()
    {
        switch (state)
        {
            case "idle":
                if (Input.GetKeyDown(attack))
                {
                    //fijar la dirección del ataque
                    if (suaveTimer >= suaveMaxTimer)
                    {
                        canGiveDamage1 = true;
                        state = "attack";
                        
                    }
                   
                }
                break;
            
            case "attack":
                //contar tiempo
                //aplicar cooldown
                //desactivar canGive
                suaveTimer -= Time.deltaTime;
                canGiveDamage1 = false;
                if (suaveTimer <= 0)
                {
                    state = "idle";
                    suaveTimer = suaveMaxTimer;
                }
                
                break;
        }
    }

    private float PlayerAtaqueDebil()
    {
        return player.ataquePlayer * 1;
        
    }
    
    public float PlayerAtaqueFuerte()
    {
        return player.ataquePlayer * 6;
        
        
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
        
        

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<IReciveDamage>(out IReciveDamage giveDamage))
        {
            if (canGiveDamage1)
            {
                giveDamage.Damage(PlayerAtaqueDebil());//se supone que deberi< efectuar el ataque del tipo que 
            }
            
        }
    }
}
