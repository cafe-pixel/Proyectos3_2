using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ItemUI : MonoBehaviour
{
    public Sprite imagenItem;
    public float duracion = 3f;

    private ManagerUI uiManager;

    private void Start()
    {
        uiManager = FindFirstObjectByType<ManagerUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.Mostrar(imagenItem, duracion);
        }
    }
}
