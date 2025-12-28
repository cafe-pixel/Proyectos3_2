using UnityEngine;

public class Assets : MonoBehaviour,IReciveDamage
{
    [SerializeField] private GameObject ItemPrefab; 
    [SerializeField] [Range(0f, 1f)] private float dropChance;
    private bool hasBeenDropped = false;

    public void Damage()
    {
        DropItem();
    }
    
    void DropItem()
    {
        hasBeenDropped = true;
        
        float randomValue = Random.value;

        if (randomValue <= dropChance && ItemPrefab != null)
        {
            Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        }
    }
}
