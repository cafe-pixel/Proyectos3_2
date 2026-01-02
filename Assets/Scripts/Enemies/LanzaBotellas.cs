using UnityEngine;

public class LanzaBotellas : EnemyAttack
{
    
    //stats
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float baseDamage = 20f;
    
    //referencias
    private DrunkGuy drunkGuy;
    
    //clase padre
    protected override float Cooldown => cooldown;

    private void Start()
    {
        drunkGuy = GetComponent<DrunkGuy>();
    }

    protected override void DoAttack()
    {
        float finalAttack =  baseDamage + drunkGuy.ataqueDrunkGuy;
    }
}
