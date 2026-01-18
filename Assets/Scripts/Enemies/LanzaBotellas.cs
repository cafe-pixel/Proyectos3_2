using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class LanzaBotellas : EnemyAttack
{
    
    //stats
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private float baseDamage = 20f;
    
    //referencias
   [SerializeField] private DrunkGuy drunkGuy;
   [SerializeField] private Animator anim;

    private float finalAttack;
    //clase padre
    protected override float Cooldown => cooldown;
    
    //lanzar botellas
    [SerializeField] private GameObject bottle;//referencia al game object de la botella]
    [SerializeField] [Range(0f,1f)] private float dropChance;
    
    [SerializeField] private float fuerzaHorizontal = 8f;
    [SerializeField] private float fuerzaVertical = 6f;

    private void Start()
    {
        
    }

    protected override void DoAttack()
    {
        finalAttack =  baseDamage + drunkGuy.ataqueDrunkGuy;
        Debug.Log("Voy a tirar la botella");
        anim.SetTrigger("hit");
        DropBottle();
        
    }

    private void DropBottle()
    {
       
        float randomValue = Random.value;
        if (randomValue <= dropChance && bottle != null)
        {
            GameObject bottleInstantiate = Instantiate(bottle, transform.position, Quaternion.identity);
            Rigidbody2D bottleRigidBody = bottleInstantiate.GetComponent<Rigidbody2D>();
            if (bottleRigidBody == null) return;
            bottleRigidBody.AddForce((player.position - transform.position).normalized * fuerzaHorizontal + Vector3.up * fuerzaVertical, ForceMode2D.Impulse);
            Bottle bottleInst = bottleInstantiate.GetComponent<Bottle>();
            bottleInst.Initialize(finalAttack);
            Debug.Log("He tirado la botella");

        }
    }

}
