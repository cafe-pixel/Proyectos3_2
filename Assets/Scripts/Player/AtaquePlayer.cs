using System;
using Unity.VisualScripting;
using UnityEngine;



public class AtaquePlayer : MonoBehaviour
{

    public enum AttackTypes
    {
        cabezazo = 0,
        swing = 1
    }
   
    private Player player;
    
    //states
    private string state = "idle";
    
    //ataque suave
    private float softDamage = 1f;
    
    //ataque fuerte
    private float hardDamage= 3f;
    
    //Jabcodo
    private float jabCodo = 115f;
    
    //Swingswing
    private float swingSwing = 165f;
    
    //Cabezazo
    private float cabezazo = 180f;
    
    //AattackDamages
    public AttackTypes attackType { get; set; }
    [SerializeField] private float cabezazoDmg; // 1
    [SerializeField] private float swingDmg; // 2

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

    public void Damage(int damage)
    {
        //Enemigo.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<IReciveDamage>(out IReciveDamage giveDamage))
        {
            // if (player.canGiveDamage1) //recibe si puede dar el ataque
            // {
            //     giveDamage.Damage(PlayerAtaqueDebil());//efectua el ataque de playerataquedebil
            // }
            //
            // if (player.canGiveDamage2)
            // {
            //     giveDamage.Damage(PlayerAtaqueFuerte());
            // }

            switch (attackType)
            {
                case AttackTypes.cabezazo:
                        giveDamage.Damage(cabezazoDmg);
                    break;
                
                case AttackTypes.swing:
                    giveDamage.Damage(swingDmg);
                    break;
            }
            
            
            
        }
    }
}
