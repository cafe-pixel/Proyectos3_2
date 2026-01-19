using UnityEngine;

public class BossHealth : MonoBehaviour,IReciveDamage
{
    [SerializeField] private float vidaMaxBoss = 100;
    private float vidaBoss;
    
    protected float defensaEnemy;
    
    
    protected virtual void EnemyRestarVida(float value)
    {
        vidaBoss -= value;
        if (vidaBoss <= 0)
        {
            Morir();
        }
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
