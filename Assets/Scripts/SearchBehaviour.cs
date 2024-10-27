using UnityEngine;
using UnityEngine.AI;

public class SearchBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private Transform target;
    private int waypointIndex = 0;
    private Vector3 rayOrigin;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        waypoints = animator.GetComponent<Agent>().waypoints;

        // Establece el destino inicialmente
        UpdateDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Posicionamiento del raycast
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0);

        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 5, Color.red); // Raycast visible (flecha roja)

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            if (hit.collider.gameObject.name == "Worker")
            {
                Debug.Log("Worker detectado, activando huida");
                animator.SetTrigger("ThiefToFlee"); // Cambia al estado de huida
                return; // Termina la actualización aquí
            }
        }

        // Si el agente ha llegado al destino, va al siguiente waypoint
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
