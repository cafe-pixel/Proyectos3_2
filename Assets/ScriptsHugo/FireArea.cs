using UnityEngine;

public class FireArea : MonoBehaviour
{
    public float duration = 2f;
    public int damagePerSecond = 10;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        /*if (other.TryGetComponent<IReciveDamage>(out IReciveDamage player) && other.CompareTag("Player"))
        {
            player.Damage(damagePerSecond);
        }*/
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Damage(damagePerSecond);
        }
    }
}
