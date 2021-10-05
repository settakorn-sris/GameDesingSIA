using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private EnemyCharecter enemy;
    [SerializeField] private float distanceStopEnemy;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = enemy.Speed;
    }
    void Update()
    {
        float dis = Vector3.Distance(transform.position, player.position);
        if (dis <= distanceStopEnemy)
        {
            StopGoToPlayer();
        }
        else
        {
            GoToPlayer();
        }
        
    }
    void GoToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }
    void StopGoToPlayer()
    {
        agent.isStopped = true;
    }

    private void Stop()
    {
        StopGoToPlayer();
    }
}
