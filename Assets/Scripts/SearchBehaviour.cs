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
        agent.isStopped = false;
        waypoints = AiDirector.instance.Thiefwaypoints;
        UpdateDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ajuste de la posición de inicio del Raycast y su dirección
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0);
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 10, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 10f))
        {
            Debug.Log("detecta raycast");

            if (hit.collider.gameObject.CompareTag("Worker"))
            {
                //Debug.Log("detecta Worker");
                animator.SetBool("ThiefToFlee", false);
                animator.SetBool("ToSearch", false);
                animator.SetBool("KO", false);
                animator.SetBool("ToHide", true); // Cambiar al estado Hide
               
                agent.ResetPath(); // Detener el movimiento hacia el waypoint
            }
            else if (hit.collider.gameObject.CompareTag("Guard"))
            {
                //Debug.Log("detecta Guard");
                animator.SetBool("ToHide", false); 
                animator.SetBool("ThiefToFlee", true);
                animator.SetBool("ToSearch", false);
                animator.SetBool("KO", false);
                agent.ResetPath(); // Detener el movimiento hacia el waypoint

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