using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IReciveDamage, IReciboObjeto
{
    [SerializeField] private float vidaMaxPlayer = 100;
    private float vidaPlayer;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        vidaPlayer = vidaMaxPlayer;
    }

    private void PlayerSumarVida() //suma vida si es que puede (bebida1)
    {
        if (!Mathf.Approximately(vidaPlayer, vidaMaxPlayer))
        {
            vidaPlayer += 25;
            if (vidaPlayer > 100)
            {
                vidaPlayer = 100;
            }
        }
        else
        {
            Debug.Log("Vida suficiente");
        }
    }

    private void PlayerSumarVida2() //le suma la bebida2
    {
        if (!Mathf.Approximately(vidaPlayer, vidaMaxPlayer))
        {
            vidaPlayer += 50;
            if (vidaPlayer > 100)
            {
                vidaPlayer = 100;
            }
        }
        else
        {
            Debug.Log("Vida suficiente");
        }
    }

    public void AplicarEfecto(Items.TipoItem item)
    {
        switch (item)
        {
            case Items.TipoItem.BebidaEnergetica:
                PlayerSumarVida();
                break;
            case Items.TipoItem.BebidaEnergetica2:
                PlayerSumarVida2();
                break;
        }
    }
    
    public void PlayerRestarVida(float damage) //le resta el daño total
    {
        vidaPlayer -= damage;
        
        if (vidaPlayer == 0)
        {
            Muerto();
        }
    }
    
    

    

    private void Muerto() //hacer el canvas
    {
       
        //Pantalla de Jugar Otra Vez

    }
    
    public void Damage(float damage) //daño recibido menos la defensa
    {
        if (!player.isParrying)
        {
            float damageFinal = damage -  player.defensaPlayer;
            PlayerRestarVida(damageFinal);
        }
        
    }
}
