using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IReciveDamage, IReciboObjeto
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float vidaMaxPlayer = 100;
    [SerializeField] private float vidaPlayer;

    private Player player;
    private Animator anim;

    private void Awake()
    {
        player = GetComponent<Player>();
        vidaPlayer = vidaMaxPlayer;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) PlayerRestarVida(10);
        if (Input.GetKeyDown(KeyCode.I)) PlayerSumarVida2();
        if (Input.GetKeyDown(KeyCode.O)) PlayerSumarVida();
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
        
        healthBar.fillAmount = vidaPlayer / vidaMaxPlayer;
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
        
        healthBar.fillAmount = vidaPlayer / vidaMaxPlayer;
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

    private void PlayerRestarVida(float damage) //le resta el daño total
    {
        vidaPlayer -= damage;
        anim.SetTrigger("hitTaken");
        
        if (vidaPlayer <= 0)
        {
            //falta por poner la animacion?
            Muerto();
        }
        
        healthBar.fillAmount = vidaPlayer / vidaMaxPlayer;
        
    }
    
    private void Muerto() //hacer el canvas
    {
       Destroy(gameObject);
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
    
    public float GetCurrentLife()
    {
        return vidaPlayer;
    }

    public float GetMaxLife()
    {
        return vidaMaxPlayer;
    }
}
