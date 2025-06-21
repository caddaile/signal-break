using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : BaseCharacter
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private int damage = 2;
    [SerializeField] private float lungeForce = 15f;
    [SerializeField] private float lungeDuration = 0.3f;
    [SerializeField] private AttackCollider attackCollider;

    private float lastAttackTime = 0f;

    private Transform target;
    private NavMeshAgent agent;
    private bool isLunging = false;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 1.5f;
    }

    void Update()
    {
        if (target != null && !isLunging)
        {
            agent.SetDestination(target.position);

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                TryAttackPlayer();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Player>())
        {
            target = other.transform.root;
        }
    }

    private void TryAttackPlayer()
    {
        lastAttackTime = Time.time;
        StartCoroutine(LungeForward());
    }

    private System.Collections.IEnumerator LungeForward()
    {
        isLunging = true;
        agent.enabled = false;

        attackCollider.enabled = true;
        attackCollider.Damage = damage;

        Vector3 lungeDirection = (target.position - transform.position).normalized;
        rb.AddForce(lungeDirection * lungeForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(lungeDuration);

        rb.linearVelocity = Vector3.zero;
        attackCollider.enabled = false;
        agent.enabled = true;
        isLunging = false;
    }

    public override void Die()
    {
        GameEvents.GoalProgress(QuestTargetType.Slain, 1);
        base.Die();
    }
}