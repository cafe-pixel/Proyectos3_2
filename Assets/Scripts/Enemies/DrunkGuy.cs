using UnityEngine;

public class DrunkGuy : Enemy
{
    //stats
    public float ataqueDrunkGuy = 10f;
    
    //rangos de ataque
    [SerializeField] private float chaseRange = 8f;
    [SerializeField] private float attackRange = 2f; //este enemigo ataca desde una distancia mayor porque realiza un ataque a larga distancia
    
    
    //clase padre
    
    protected override float ChaseRange => chaseRange;
    protected override float AttackRange => attackRange;
}
