using UnityEngine;

public class Puñetazos : EnemyAttack
{
    //numero de puñetazos
    [SerializeField] [Range(1, 2)] int punch;
    
    //stats
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private float baseDamage = 10f;
    
    //referencias
    public Biker biker;
    
    //clase padre
    protected override float Cooldown => cooldown; //manda el valor del cooldown


    private void Start()
    {
        biker = GetComponent<Biker>();
    }

    protected override void DoAttack()
    {
        float finalDamage = baseDamage + biker.ataqueBiker;
        
    }
}
