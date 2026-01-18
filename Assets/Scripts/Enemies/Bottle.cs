using Unity.VisualScripting;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    private float damage = 20;

    public void Initialize(float target)
    {
        this.damage = target;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.TryGetComponent<IReciveDamage>(out IReciveDamage giveDamage))
        {
            giveDamage.Damage(damage);
            Destroy(gameObject);
        }
    }
}
