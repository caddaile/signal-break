using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : BaseCharacter
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private int damage = 2;

    private float lastAttackTime = 0f;

    private Transform target;
    private NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 1.5f;
    }

    void Update()
    {
        if (target != null)
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
        var player = target.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

    public override void Die()
    {
        GameEvents.GoalProgress(QuestTargetType.Slain, 1);
        base.Die();
    }
}