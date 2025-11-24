using UnityEngine;

public class Enemy : MonoBehaviour,IGiveDamage
{
    public int VidaEnemy { get; set; } = 1;
    public int Defensa { get; set; } = 1;
    public int Ataque { get; set; } = 1;
    
    
    public void EnemySumarVida(int value)
    {
        
        
    }
    
    public void EnemyRestarVida(int value)
    {
        
    }
    
    public void Damage()
    {
        EnemyRestarVida(1);
    }
}




