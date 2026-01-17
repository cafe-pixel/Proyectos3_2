using UnityEngine;

public class Pruebayuyu : MonoBehaviour
{
    private string state = "move";

    [SerializeField] private Transform attackPoint;

    [Header("Punch")] 
    [SerializeField] private float punchDamage;
    [SerializeField] private float maxPunchTimer;
    private float punchTimer;
    
    void Update()
    {
        switch (state)
        {
            case "move":

                if (Input.GetKeyDown(KeyCode.J))
                {
                    Attack(punchDamage);
                    punchTimer = maxPunchTimer;
                    state = "punch";
                }

                break;
            
            case "punch":

                punchTimer -= Time.deltaTime;

                if (punchTimer <= 0)
                {
                    state = "move";
                }

                break;
        }
    }

    public void Attack(float damage)
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(attackPoint.position, 1);

        foreach (var c in col)
        {
            if (c.TryGetComponent<IReciveDamage>(out IReciveDamage e))
            {
                e.Damage(damage);
            }
        }
    }
}
