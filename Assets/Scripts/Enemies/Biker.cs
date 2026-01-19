using System;
using UnityEngine;

public class Biker : Enemy
{
    //stats
    public float ataqueBiker = 5f;
    
    //rangos de ataque
    [SerializeField] private float chaseRange = 15f;
    [SerializeField] private float attackRange = 2f;  //ataca en una distancia corta al ser un ataque cuerpo a cuerpo
    
    //referencias
    [SerializeField] private Puñetazos puñetazos;

    //clases padres
    protected override float ChaseRange => chaseRange;
    protected override float AttackRange => attackRange;

    protected override void Start()
    {
        base.Start();
        
        ataqueEnemy = ataqueBiker;
        defensaEnemy = 2f;
        velocidadEnemy = 3f;
        
        
    }

    public void MakeHit() //le llamo desde inspeector xd
    {
        puñetazos.Hit();
    }

   
}
