using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    Transform camTransform;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
        if (currentHealth < maxHealth) GetComponent<Canvas>().enabled = true;
    }

    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        var camForward = camTransform.forward;

        if (camForward.sqrMagnitude > 0.001f)
        {
            var upwardTilt = Quaternion.Euler(0f, 0f, 0f); // Adjust tilt angle here
            transform.rotation = Quaternion.LookRotation(camForward) * upwardTilt;
        }
    }
}
