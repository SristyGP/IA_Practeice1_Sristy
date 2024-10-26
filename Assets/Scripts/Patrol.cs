using UnityEngine.AI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Patrol :StateMachineBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    public Transform target;
    private Vector3 Raycast; 




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
        Debug.DrawRay(Raycast, animator.transform.forward * 5, UnityEngine.Color.red); //  Raycast visible (flecha roja)
        Raycast = animator.transform.position + new Vector3(0, 1f, 0);// posicionamiento de raycast sobre el suelo

        RaycastHit hit;
        if (Physics.Raycast(Raycast, animator.transform.forward, out hit, 5f)) // detecata al ladron
        {
            Debug.Log("detección");
            if (hit.transform.CompareTag("Thief"))
            {
                animator.SetBool("huida", true);
                Debug.Log("huye por tu vida");
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
        Debug.Log("patrullando");
        waypointIndex = (waypointIndex + 1) % waypoints.Length; 
      
        UpdateDestination(); 
    }
}
