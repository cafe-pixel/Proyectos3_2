using UnityEngine;

public class Enemy : MonoBehaviour,IReciveDamage
{
   private float vidaEnemy; 
   private float defensaEnemy;
   private float ataqueEnemy;
    
    
    public void EnemySumarVida(int value)
    {
        
        
    }
    
    private void EnemyRestarVida(float value)
    {
        vidaEnemy -= value;
        if(vidaEnemy<=0) Morir();
    }
    
    public void Damage(float damage)
    {
        float damageFinal = damage - defensaEnemy;
        EnemyRestarVida(damageFinal);
    }

    private void Morir()
    {
        Destroy(gameObject);
    }
}




