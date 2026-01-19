using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy prefabs")]
    [SerializeField] private GameObject bikerEnemy;
    [SerializeField] private GameObject drunkEnemy;

    [Header("Cantidad de enemigos")]
    [SerializeField] private int amountBikerEnemy;
    [SerializeField] private int amountDrunkEnemy;

    [Header("Spawn area")]
    [SerializeField] private Vector2 areaCenter;
    [SerializeField] private Vector2 areaSize;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            SpawnEnemies(bikerEnemy, amountBikerEnemy);
            SpawnEnemies(drunkEnemy, amountDrunkEnemy);

            Destroy(gameObject); 
        }
    }

    private void SpawnEnemies(GameObject enemyPrefab, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
                Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2)
            );

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }
    
}
