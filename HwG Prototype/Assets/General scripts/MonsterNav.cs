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

    private float detectionRange = 5f;
    private float viewAngle = 90f;
    private float losePlayerTime = 3f;
    private float timeSincelostPlayer;

    private EnemyState state = EnemyState.Patrolling;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(player.position, transform.position);

        switch (state)
        {
            case EnemyState.Patrolling:
                GotoNextPoint();
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

        return true;
    }

    void ChasePlayer()
    {
        agent.destination = player.position;
    }
}
