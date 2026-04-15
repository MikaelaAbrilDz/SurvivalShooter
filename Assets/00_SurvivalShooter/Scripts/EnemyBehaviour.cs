using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject target;
    
    NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(target.transform.position, out navHit, 5, NavMesh.AllAreas);
        agent.SetDestination(navHit.position);
    }
}
