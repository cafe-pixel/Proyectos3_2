using UnityEngine;

public class BossHealth : MonoBehaviour,IReciveDamage
{
    protected float vidaEnemy; 
    [SerializeField] private float vidaMaxEnemy;
    protected float defensaEnemy;
    [SerializeField] private GameObject bossGameObject;
    
    protected virtual void EnemyRestarVida(float value)
    {
        vidaEnemy -= value;
        if (vidaEnemy <= 0)
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
        Destroy(bossGameObject.gameObject);
    }
    
    
}
