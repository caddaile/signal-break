using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 2f;
    public GameObject trail;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.root.CompareTag("Player"))
        {
            if (other.GetComponent<SphereCollider>()) return;

            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
                knockbackDir.y = 0f;
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(knockbackDir * 500f);
            }

            BaseCharacter hit = other.GetComponentInParent<BaseCharacter>();
            if (hit)
            {
                bool isDead = hit.TakeDamage(damage);
                if (isDead) hit.Die();
            }

            trail.transform.parent = null;
            Destroy(trail, 1f);

            Destroy(gameObject);
        }
    }
}