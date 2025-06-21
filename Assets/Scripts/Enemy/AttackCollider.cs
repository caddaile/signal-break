using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.transform.root.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(Damage);
            enabled = false; // prevent multiple hits per lunge
        }
    }
}