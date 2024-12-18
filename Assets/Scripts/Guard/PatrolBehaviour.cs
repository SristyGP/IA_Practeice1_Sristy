using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    private int waypointIndex;
    private Vector3 rayOrigin;
    private bool alarmActivated = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        waypoints = AiDirector.instance.Guardwaypoints;
        UpdateDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ajuste de la posici�n de inicio del Raycast y su direcci�n
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0);
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 10, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 10f))
        {
            //Debug.Log("detecta raycast");

            if (hit.collider.gameObject.CompareTag("Thief"))
            {
                //Debug.Log("detecta Thief");
                animator.SetBool("ToPursue", true); // Cambiar al estado Persue
                 // Detener el movimiento hacia el waypoint
            }
            else if(hit.collider.gameObject.CompareTag("Thief"))
            {

            }
            

        }

        RaycastHit hitclose;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hitclose, 2f))
        {
            Debug.Log("detecta el estado KO");


            if (hitclose.collider.gameObject.CompareTag("Thief"))
            {
                Debug.Log("cambia a estado ko");
                animator.SetBool("ToHide", false); // Cambiar al estado Hide
                animator.SetBool("KO", true);
                animator.SetBool("ToSearch", false);
                animator.SetBool("ToSearch", false);
                agent.ResetPath(); // Detener el movimiento hacia el waypoint

            }

            if (hitclose.collider.gameObject.CompareTag("Thief"))
            {
                Debug.Log("cambia a estado Attack");
                animator.SetBool("ToAttack", true); // Cambiar al estado Attack
                animator.SetBool("ToPursue", false);
               
                agent.ResetPath(); // Detener el movimiento hacia el waypoint

            }

            if (!alarmActivated && !agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Debug.Log("cambia de estado alarmaGuard");
                alarmActivated = true; // Evita activar la alarma m�ltiples veces
                animator.SetBool("AlarmGuard", true);

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

