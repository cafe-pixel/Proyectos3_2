using System;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
   
    private Player player;
    
    //states
    private string state = "idle";
    
    //ataque suave
    private float softDamage = 1f;
    
    //ataque fuerte
    private float hardDamage= 3f;
    
    

    private void Awake()
    { 
        player = GetComponent<Player>();
        

    }

  

    private float PlayerAtaqueDebil()
    {
        return player.ataquePlayer * softDamage;
        
    }

    private float PlayerAtaqueFuerte()
    {
        return player.ataquePlayer * hardDamage;
        
        
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<IReciveDamage>(out IReciveDamage giveDamage))
        {
            if (player.canGiveDamage1) //recibe si puede dar el ataque
            {
                giveDamage.Damage(PlayerAtaqueDebil());//efectua el ataque de playerataquedebil
            }
            
            if (player.canGiveDamage2)
            {
                giveDamage.Damage(PlayerAtaqueFuerte());
            }
            
        }
    }
}
