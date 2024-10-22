using UnityEngine.AI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Patrol :StateMachineBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    public Transform target;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agent = animator.GetComponent<NavMeshAgent>();
        waypoints= animator.GetComponent<Agent>().waypoints;
        UpdateDestination();

    }

    // Update is called once per frame
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            GoToNextWaypoint(); 
        }
  

    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex]; 
        agent.SetDestination(target.position); 
    }

    void GoToNextWaypoint()
    {
        waypointIndex = (waypointIndex + 1) % waypoints.Length; 
        UpdateDestination(); 
    }
}
