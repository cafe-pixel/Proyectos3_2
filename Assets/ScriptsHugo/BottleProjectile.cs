using UnityEngine;

public class BottleProjectile : MonoBehaviour
{
    public GameObject fireAreaPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(fireAreaPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
