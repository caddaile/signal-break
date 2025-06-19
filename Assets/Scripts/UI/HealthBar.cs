using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
        if (currentHealth < maxHealth) GetComponent<Canvas>().enabled = true;
    }

    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
