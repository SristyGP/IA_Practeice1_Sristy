using UnityEngine;
using UnityEngine.AI;

public class SearchBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    private int waypointIndex;
    private Vector3 rayOrigin;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        waypoints = animator.GetComponent<Agent>().waypoints;
        UpdateDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ajuste de la posición de inicio del Raycast y su dirección
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0);
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 5, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            Debug.Log("detecta raycast");
            // Verifica si el objeto detectado es un "Worker"
            if (hit.collider.gameObject.name == "Worker")
            {
                Debug.Log("detecta Worker");
                animator.SetTrigger("ThiefToFlee");
                animator.ResetTrigger("ToHide");
                animator.ResetTrigger("ToSearch");
            }
            // Verifica si el objeto detectado es un "Guard"
            else if (hit.collider.gameObject.name == "Guard")
            {
                Debug.Log("detecta Guardr");
                animator.SetTrigger("ToHide");
                animator.ResetTrigger("ThiefToFlee");
                animator.ResetTrigger("ToSearch");
            }
        }

        // Si ha alcanzado el destino, se mueve al siguiente waypoint
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            GoToNextWaypoint();
        }
    }

    void UpdateDestination()
    {
        // Actualiza el destino al siguiente waypoint
        agent.SetDestination(waypoints[waypointIndex].position);
    }

    void GoToNextWaypoint()
    {
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
        UpdateDestination();
    }
}