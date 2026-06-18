using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ItemUI : MonoBehaviour
{
    public Image imagenUI;
    
    public float duracion = 3f;

    private Coroutine rutinaActual;

    private void Start()
    {
        if (imagenUI != null)
        {
            imagenUI.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rutinaActual != null)
            {
                StopCoroutine(rutinaActual);
            }

            rutinaActual = StartCoroutine(MostrarImagen());
        }
    }

    private IEnumerator MostrarImagen()
    {
        imagenUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(duracion);

        imagenUI.gameObject.SetActive(false);
    }
}
