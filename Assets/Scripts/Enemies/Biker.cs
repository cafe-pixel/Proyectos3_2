using UnityEngine;

public class Biker : Enemy
{
    //stats
    public float ataqueBiker = 5f;
    
    //rangos de ataque
    [SerializeField] private float chaseRange = 15f;
    [SerializeField] private float attackRange = 2f;  //ataca en una distancia corta al ser un ataque cuerpo a cuerpo
    
    
    //clases padres
    protected override float ChaseRange => chaseRange;
    protected override float AttackRange => attackRange;
}
