using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public float damage;
    public float cooldown;

    public float lastAttackTime; //momento en el que el enemigo hizo su último ataque

    public void TryAttack()
    {
        if (Time.time < lastAttackTime + cooldown)
            return; //Si el tiempo del juego es menor que el ultimo ataque + el cooldown no permite realizar el ataque
        DoAttack();
        lastAttackTime = Time.time; //Setea el ultimo ataque en el tiempo real de juego      
    }

    protected abstract void DoAttack();
}
   
