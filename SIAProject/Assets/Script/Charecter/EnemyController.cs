using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform player;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
