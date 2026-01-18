using UnityEngine;

public class Assets : MonoBehaviour,IReciveDamage
{
    [SerializeField] private GameObject ItemPrefab; 
    [SerializeField] [Range(0f, 1f)] private float dropChance;
    private bool hasBeenDropped = false;
    [SerializeField] private int maxHits = 4;
    private int hits = 0;

    public void Damage()
    {
        DropItem();
    }
    
    void DropItem()
    {
        hits++;
        hasBeenDropped = true;
        
        float randomValue = Random.value;

        if (randomValue <= dropChance && ItemPrefab != null)
        {
            Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        }
        
        if (hits == maxHits) Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
