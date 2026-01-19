using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Assets : MonoBehaviour,IReciveDamage
{
    [SerializeField] private GameObject[] itemPrefab; 
    [SerializeField] [Range(0f, 1f)] private float dropChance;
    private bool hasBeenDropped = false;
    [SerializeField] private int maxHits = 4;
    private int hits = 0;

    private Animator anim;
    
    private List<int> xUsadas = new List<int>();

    private void Start()
    {
        anim = GetComponent<Animator>();
        xUsadas.Clear();
    }

    public void Damage(float damage)
    {
        DropItem();
    }
    
    void DropItem()
    {
        hits++;
        anim.SetInteger("hitNumber",hits);
        hasBeenDropped = true;
        
        float randomValue = Random.value;

        if (itemPrefab != null && randomValue <= dropChance)
        {
            int zonaRandomEnX = TomarNumeroRandomSinRepetir(-2, 1);
            
            
            int indexGameObject = Random.Range(0, itemPrefab.Length);
            GameObject instantiateGameObject = itemPrefab[indexGameObject];
            
            Instantiate(instantiateGameObject, this.transform.position + new Vector3(zonaRandomEnX,-1,0), Quaternion.identity);
            
            
        }


        if (hits == maxHits) Kill();
    }

    private int TomarNumeroRandomSinRepetir(int min, int max)
    {
        int x;

        do
        {
            x = Random.Range(min, max);
        } while (xUsadas.Contains(x)); //mientras que la lista de xUsadas no contenga la nueva x se repite el bucle
        
        xUsadas.Add(x);//se añade a la lista para la siguiente 
        
        return x;
    }
    

    private void Kill()
    {
        Destroy(gameObject);
    }
}
