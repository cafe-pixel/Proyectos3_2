using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage; 

    private float fadeSpeed = 5f;
    
    void Start()
    {
        if (player != null && healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = player.GetMaxLife();
            healthSlider.value = player.GetCurrentLife();
        }
    }

    void Update()
    {
        if (player == null || healthSlider == null || fillImage == null) return;

        float currentHealth = Mathf.Clamp(player.GetCurrentLife(), 0, player.GetMaxLife());
        healthSlider.value = currentHealth;
        
        fillImage.fillAmount = currentHealth / player.GetMaxLife();

        //Desvanecimiento suave
        Color c = fillImage.color;
        float targetAlpha = currentHealth > 0 ? 1f : 0f;
        c.a = Mathf.MoveTowards(c.a, targetAlpha, fadeSpeed * Time.deltaTime);
        fillImage.color = c;
    }
}
