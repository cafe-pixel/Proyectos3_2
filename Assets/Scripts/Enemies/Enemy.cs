using System.Collections;
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
   protected Animator anim;
   private Rigidbody2D rb;
   private SpriteRenderer spr;
   
    //stats
    protected float vidaEnemy; 
    private float vidaMaxEnemy = 100;
   protected float defensaEnemy;
   protected float ataqueEnemy;
   protected float velocidadEnemy;
   
   //states
   private string state = "patrol";
   public PatrolSystem patrol;
   [SerializeField] EnemyAttack enemyAttack;
   private float maxAttackTimer = 1.3f;
   private float attackTimer;


   protected virtual void Start()
   {
       vidaEnemy = vidaMaxEnemy;

       rb = GetComponent<Rigidbody2D>();
       
       
       patrol = GetComponent<PatrolSystem>();
       anim = GetComponent<Animator>();

       attackTimer = maxAttackTimer;

       spr = GetComponent<SpriteRenderer>();
   }
    
   private void Update() //update ejecuta cada frame, NO USAR WHILE
   {
       
       
       
       bool inChase = PlayerInChaseRange();
       bool inAttack = PlayerInAttackRange();
       
       switch (state)
       {
           case "patrol":
               anim.SetBool("isWalking",true);
               patrol.Patrol();
               if(inChase) state = "chase";
               
               break;
           
           case "chase":
               anim.SetBool("isWalking",true);
               if(inChase)Chase();

               if (inAttack)
               {
                   attackTimer = maxAttackTimer;
                   state = "attack";
               }
               
               else if (!inChase) state = "patrol";
               break;
           
           case "attack":
               anim.SetBool("isWalking", false);

               attackTimer -= Time.deltaTime;

               if (attackTimer <= 0)
               {
                   attackTimer = maxAttackTimer;
                   
                   if (inAttack)
                   {
                       Debug.Log(enemyAttack);
                       Debug.Log(player);
                       if (transform.position.x > player.position.x)//mira si mi posicion es superior a la del punto de patrulla, mi objetivo está a la izquierda
                       {
                           transform.localScale = new Vector3(1, 1, 1);
                       }
                       else
                       {
                           transform.localScale = new Vector3(-1, 1, 1);
                       }
                       
                       
                       enemyAttack.SetTarget(player);
                       enemyAttack.TryAttack();
                   }
               
                   else state = "chase";
               }
               
               break;
       }
   }

   private void Chase()
   {
       transform.position = Vector3.MoveTowards(transform.position, player.position, velocidadEnemy * Time.deltaTime);
       if (transform.position.x > player.position.x)//mira si mi posicion es superior a la del punto de patrulla, mi objetivo está a la izquierda
       {
           transform.localScale = new Vector3(1, 1, 1);
       }
       else
       {
           transform.localScale = new Vector3(-1, 1, 1);
       }
   }

   private bool PlayerInChaseRange()
   {
       Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ChaseRange, playerLayer); 
       if (colliders.Length > 0) //si el array de colliders es mayor que cero porque el overlapSphere detecta colision en una posicion dentro del radio y de la layer indicada
       {
           player =  colliders[0].transform; //toma el transform del collider que ha recogido y lo mete en el player
           return true;
       }

       
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
      
       return false;
   }
   
    
    protected virtual void EnemyRestarVida(float value)
    {
        vidaEnemy -= value;
        anim.SetTrigger("hitTaken");
        if (vidaEnemy <= 0)
        {
            anim.SetTrigger("death");
        }
    }
    
    public void Damage(float damage)
    {
        float damageFinal = damage - defensaEnemy;
        EnemyRestarVida(damageFinal);
    }

    private void Morir() //se enciende en el animator
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}




