using System.Collections;
using UnityEngine;

public class Puñetazos : EnemyAttack
{
    //numero de puñetazos
    [SerializeField, Range(1, 2)] private int maxPunch = 2;
    
    //stats
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float timeBetweenPunches = 0.25f;
    
    //referencias
    [SerializeField] Biker biker;
    [SerializeField] Animator anim;
    private float finalDamage;
    
    //clase padre
    protected override float Cooldown => cooldown; //manda el valor del cooldown
    
    //Corrutinas
    private Coroutine attackPunches;


    private void Start()
    {
        
        
    }

    protected override void DoAttack()
    {
        if (!player || attackPunches != null) return;
        attackPunches = StartCoroutine(Punches());


    }

    public void Hit() //le llama desde el biker
    {
        if (!player) return;

        if (player.TryGetComponent<IReciveDamage>(out IReciveDamage playerHealth) && player.CompareTag("Player"))
        {
            playerHealth.Damage(finalDamage);
        }
        
    }

    private IEnumerator Punches()
    {
        Debug.Log("awe");
        finalDamage = baseDamage + biker.ataqueBiker;
        
        int punches = Random.Range(1, maxPunch + 1);
        for (int i = 0; i < punches; i++)
        {
            //Animamtor.SetInteger("comboIndex", i)
            anim.SetTrigger("hit");
            yield return new WaitForSeconds(0.01f); //mini dilay para q arranque animacion
            yield return new WaitForSeconds(timeBetweenPunches);
        }
        //Animator.SetInteger("comboIndex", i);
        attackPunches = null;
    }
}
