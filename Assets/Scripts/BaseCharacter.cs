using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseCharacter : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public float maxHealth = 10f;
    private float currentHealth;
    private Rigidbody rb;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 10f;
    }

    // Takes damage, returns if it is now dead 
    public bool TakeDamage(float damage)
    {
        currentHealth -= MathF.Max(0, damage);

        UpdateHealthBar();

        return currentHealth <= 0;
    }

    public void Die()
    {
        GameEvents.GoalProgress("kill_infected", 1);
        Destroy(gameObject);
    }

    void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}