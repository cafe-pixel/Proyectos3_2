using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Assets : MonoBehaviour,IReciveDamage
{
    [SerializeField] private GameObject[] itemPrefab;
    [SerializeField] [Range(0f, 1f)] private float dropChance;

    [Header("Área de Drop")]
    [SerializeField] private Vector2 areaSize = new Vector2(4f, 2f);
    [SerializeField] private Vector2 areaOffset = new Vector2(0f, -1f);

    [SerializeField] private int maxHits = 4;

    private int hits = 0;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Damage(float damage)
    {
        DropItem();
    }

    private void DropItem()
    {
        hits++;

        anim.SetInteger("hitNumber", hits);

        float randomValue = Random.value;

        if (itemPrefab != null &&
            itemPrefab.Length > 0 &&
            randomValue <= dropChance)
        {
            // Centro del área de drop
            Vector3 center = transform.position + (Vector3)areaOffset;

            // Posición aleatoria dentro del rectángulo
            float randomX = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float randomY = Random.Range(-areaSize.y / 2f, areaSize.y / 2f);

            Vector3 randomPosition = center + new Vector3(randomX, randomY, 0f);

            // Elegir prefab aleatorio
            int indexGameObject = Random.Range(0, itemPrefab.Length);
            GameObject instantiateGameObject = itemPrefab[indexGameObject];

            Instantiate(instantiateGameObject, randomPosition, Quaternion.identity);
        }

        if (hits >= maxHits)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 center = transform.position + (Vector3)areaOffset;

        Gizmos.DrawWireCube(
            center,
            new Vector3(areaSize.x, areaSize.y, 0f)
        );
    }
}
