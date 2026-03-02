using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum EnemyState
{
    Patrolling,
    Following
}
public class MonsterNav : MonoBehaviour
{
    public Transform player;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    [SerializeField] private float waitTime = 2f;
    [SerializeField] private bool  isWaiting;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float losePlayerTime = 3f;
    [SerializeField] private float timeSincelostPlayer;

    private EnemyState state = EnemyState.Patrolling;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.autoBraking = false;
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(player.position, transform.position);
        
        switch (state)
        {
            case EnemyState.Patrolling:
                //GotoNextPoint();
                Patrol();
                if (playerDistance <= detectionRange && PlayerSeen())
                {
                    state = EnemyState.Following;
                }
                break;
            case EnemyState.Following:
                ChasePlayer();
                if (!PlayerSeen())
                {
                    timeSincelostPlayer += Time.deltaTime;
                    if (timeSincelostPlayer >= losePlayerTime)
                    {
                        state = EnemyState.Patrolling;
                        GotoClosestPoint();
                    }
                }
                else
                {
                    timeSincelostPlayer = 0f;
                }
                break;
        }
    }

    void Patrol()
    {
        if (isWaiting)
            return;

        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            StartCoroutine(Wait());
        }
    }
    void GotoNextPoint()
    {
        if (agent.remainingDistance < 0.5f)
        {
            if (points.Length == 0)
                return;

            agent.destination = points[destPoint].position;

            destPoint = (destPoint + 1) % points.Length;
        }
    }

    private IEnumerator Wait()
    {
        isWaiting = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(waitTime);

        agent.isStopped = false;
        GotoNextPoint();
        isWaiting = false;
    }

    void GotoClosestPoint()
    {
        if (points.Length == 0)
            return;

        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < points.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, points[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        destPoint = closestIndex;
        agent.destination = points[destPoint].position;
    }

    bool PlayerSeen()
    {
        return FacingPlayer() && CanReachPlayer();
    }

    bool FacingPlayer()
    {
        Vector3 playerDirection = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, playerDirection);
        return angle <= viewAngle / 2f;
    }

    bool CanReachPlayer()
    {
        Vector3 playerDirection = player.position - transform.position;
        if (Physics.Raycast(transform.position, playerDirection.normalized, out RaycastHit hit, playerDirection.magnitude))
        {
            return hit.transform == player;
        }
        Debug.DrawRay(transform.position, playerDirection, Color.green);
        return true;
    }

    void ChasePlayer()
    {
        agent.destination = player.position;
    }
}
