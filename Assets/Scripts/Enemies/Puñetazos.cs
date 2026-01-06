using UnityEngine;

public class Puñetazos : EnemyAttack
{
    //numero de puñetazos
    [SerializeField, Range(1, 2)] private int maxPunch = 2;
    
    //stats
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private float baseDamage = 10f;
    
    //referencias
    public Biker biker;
    
    private float finalDamage;
    
    //clase padre
    protected override float Cooldown => cooldown; //manda el valor del cooldown


    private void Start()
    {
        biker = GetComponent<Biker>();
    }

    protected override void DoAttack()
    {
        finalDamage = baseDamage + biker.ataqueBiker;
        Hit();

    }

    private void Hit()
    {
        float randomValue = Random.value;
        
    }
}
