using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Combat")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float rotationSpeed = 120f;

    [Header("Detection")]
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    [Header("Patrol")]
    public Transform[] waypoints;
    private int currentWaypointIndex;

    public void Awake()
    {
        player = GameObject.Find("VR Player").transform;
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = 0.2f;
        agent.autoBraking = false;

        if (waypoints.Length > 0)
            agent.SetDestination(waypoints[0].position);

    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSight && !playerInAttackRange) Patrol();
        if (playerInSight && !playerInAttackRange) ChasePlayer();
        if (playerInSight && playerInAttackRange) AttackPlayer();
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        agent.isStopped = false;
        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                agent.SetDestination(waypoints[currentWaypointIndex].position);
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Debug.LogWarning("Player insight!");
    }

    private void AttackPlayer()
    {
        agent.SetDestination(player.position);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        if (!alreadyAttacked)
        {
            //Attack logic

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
