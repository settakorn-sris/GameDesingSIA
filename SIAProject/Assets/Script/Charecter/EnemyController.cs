using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private EnemyCharecter enemy;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = enemy.Speed;
    }
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
