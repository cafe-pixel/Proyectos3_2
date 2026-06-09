using UnityEngine;

public class FireArea : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    [SerializeField] private float damagePerSecond = 10f;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        IReciveDamage target = other.GetComponentInParent<IReciveDamage>();

        if (target != null)
        {
            target.Damage(damagePerSecond * Time.deltaTime);
        }
    }
}
