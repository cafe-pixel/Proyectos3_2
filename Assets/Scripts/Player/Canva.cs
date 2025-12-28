using TMPro;
using UnityEngine;

public class Canva : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI textM;

    private void Start()
    {
        canvas.SetActive(false);
    }

    public void CanvasAppear(string text)
    {
        canvas.SetActive(true);
        textM.text = text;
    }
}
