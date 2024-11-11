using UnityEngine;
using UnityEngine.AI;


public class PosingBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    public Transform target;
    private Vector3 rayOrigin; 

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("comienza el estado de flee");
        agent = animator.GetComponent<NavMeshAgent>();
        waypoints = animator.GetComponent<Agent>().waypoints; 
        UpdateDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 10, Color.red); // Raycast visible (flecha roja)
        rayOrigin = animator.transform.position + new Vector3(0, 0.5f, 0); // Posicionamiento de raycast - 1 metro es deamsiado alto

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
           //Debug.Log("detección");
            if (hit.collider.gameObject.CompareTag ("Thief")) 
            {
                Debug.Log("Thief detectado, activando huida");
                animator.SetBool("ToPosing", false);
                animator.SetBool("ToAffraid", false);
                animator.SetBool("ToFlee", true);
                
            }
        }

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
