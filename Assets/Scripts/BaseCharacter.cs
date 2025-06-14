using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseCharacter : MonoBehaviour
{
    public float Health = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 10f;
    }

    // Takes damage, returns if it is now dead 
    public bool TakeDamage(float damage)
    {
        Health -= MathF.Max(0, damage);

        return Health <= 0;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}