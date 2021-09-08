using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Transform player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
