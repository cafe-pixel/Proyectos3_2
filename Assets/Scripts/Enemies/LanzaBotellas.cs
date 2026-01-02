using UnityEngine;

public class LanzaBotellas : EnemyAttack
{
    
    //stats
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private float baseDamage = 20f;
    
    //referencias
    private DrunkGuy drunkGuy;
    
    //clase padre
    protected override float Cooldown => cooldown;
    
    //lanzar botellas
    [SerializeField] private GameObject bottle;//referencia al game object de la botella]
    [SerializeField] [Range(0f,1f)] private float dropChance;
    private bool hasBeenDropped;

    private void Start()
    {
        drunkGuy = GetComponent<DrunkGuy>();
    }

    protected override void DoAttack()
    {
        float finalAttack =  baseDamage + drunkGuy.ataqueDrunkGuy;
        //esto debe lanzar una botella haciendo una parábola
        DropBottle();
    }

    private void DropBottle()
    {
        hasBeenDropped = true;
        float randomValue = Random.value;
        if (randomValue <= dropChance && bottle != null)
        {
            Instantiate(bottle, transform.position, Quaternion.identity);
            //ahora hay que hacer que cuando se instancie busque el lugar del jugador en el momento en el que se instancia y le golpee haciendo una parabola
        }
    }
}
