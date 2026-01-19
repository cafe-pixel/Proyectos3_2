using Unity.VisualScripting;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    private float damage = 5;

    public void Initialize(float target)
    {
        this.damage = target;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.TryGetComponent<IReciveDamage>(out IReciveDamage giveDamage) && (collision.CompareTag("Player")||collision.CompareTag("Limit")))
        {
            giveDamage.Damage(damage);
            Destroy(gameObject);
        }
    }
}
