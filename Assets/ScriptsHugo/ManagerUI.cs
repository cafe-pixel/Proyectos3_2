using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    public Image imagenUI;

    private Coroutine rutina;

    private void Start()
    {
        imagenUI.gameObject.SetActive(false);
    }

    public void Mostrar(Sprite sprite, float duracion)
    {
        if (rutina != null)
            StopCoroutine(rutina);

        rutina = StartCoroutine(MostrarRutina(sprite, duracion));
    }

    private IEnumerator MostrarRutina(Sprite sprite, float duracion)
    {
        imagenUI.sprite = sprite;
        imagenUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(duracion);

        imagenUI.gameObject.SetActive(false);
    }
}
