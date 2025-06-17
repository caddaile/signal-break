using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : BaseCharacter
{
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
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    public override void Die()
    {
        GameEvents.GoalProgress(QuestTargetType.KillInfected, 1);
        base.Die();
    }
}