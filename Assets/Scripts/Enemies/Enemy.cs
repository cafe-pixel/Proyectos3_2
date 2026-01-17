using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,IReciveDamage
{
    
    //rangos de vision
   protected abstract float ChaseRange { get; }
   protected abstract float AttackRange { get; }
   [SerializeField] private LayerMask playerLayer; //poner el jugador en la layer del jugador
   
   //referencias
   private Transform player; //va a pillar el transform de lo que llames player
    
    //stats
    private float vidaEnemy;
    private float vidaMaxEnemy = 100;
   protected float defensaEnemy;
   protected float ataqueEnemy;
   protected float velocidadEnemy;
   
   //states
   private string state = "patrol";
   public PatrolSystem patrol;
   public EnemyAttack enemyAttack;


   private void Awake()
   {
       vidaEnemy = vidaMaxEnemy;
       
       patrol = GetComponent<PatrolSystem>();
       enemyAttack = GetComponent<EnemyAttack>();
   }
    
   private void Update() //update ejecuta cada frame, NO USAR WHILE
   {
       bool inChase = PlayerInChaseRange();
       bool inAttack = PlayerInAttackRange();
       
       switch (state)
       {
           case "patrol":
               patrol.Patrol();
               if(inChase) state = "chase";
               
               break;
           
           case "chase":
               if(inChase)Chase();
               
               if (inAttack) state = "attack";
               
               else if (!inChase) state = "patrol";
               break;
           
           case "attack":
               if (inAttack)
               {
                   enemyAttack.SetTarget(player);
                   enemyAttack.TryAttack();
               }
               
               else state = "chase";
               break;
       }
   }

   private void Chase()
   {
       transform.position = Vector3.MoveTowards(transform.position, player.position, velocidadEnemy * Time.deltaTime);
   }

   private bool PlayerInChaseRange()
   {
       Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ChaseRange, playerLayer); 
       if (colliders.Length > 0) //si el array de colliders es mayor que cero porque el overlapSphere detecta colision en una posicion dentro del radio y de la layer indicada
       {
           player =  colliders[0].transform; //toma el transform del collider que ha recogido y lo mete en el player
           return true;
       }

       player = null;
       return false;
   }

   private bool PlayerInAttackRange()
   {
       Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AttackRange, playerLayer);
       if (colliders.Length > 0)
       {
           player =  colliders[0].transform;
           return true;
       }
       player = null;
       return false;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}




