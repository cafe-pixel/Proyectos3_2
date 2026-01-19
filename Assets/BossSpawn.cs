using System;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{

    [SerializeField] private GameObject boss;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            boss.SetActive(true);
            Destroy(gameObject);
        }
    }
}
