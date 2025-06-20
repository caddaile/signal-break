using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseCharacter : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public float maxHealth = 10f;
    protected float currentHealth;
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
        Debug.Log($"{transform.name} taking {damage} damage, health is {currentHealth}");

        UpdateHealthBar();

        return currentHealth <= 0;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void UpdateHealthBar()
    {
        if (healthBar)
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}