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
        agent = animator.GetComponent<NavMeshAgent>();
        waypoints = animator.GetComponent<Agent>().waypoints;
        UpdateDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Raycast para detección del Thief
        rayOrigin = animator.transform.position + new Vector3(0, 0.5f, 0);
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 10, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            if (hit.collider.CompareTag("Thief")) // Verifica si el objeto tiene el tag "Thief"
            {
                Debug.Log("Thief detectado, activando alarma y huida");

                // Notificar al AiDirector para activar la alarma
                AiDirector.instance.TriggerAlarm(animator.transform.position);

                // Cambiar a estado de huida
                animator.SetBool("ToPosing", false);
                animator.SetBool("ToAffraid", false);
                animator.SetBool("ToFlee", true);

                agent.ResetPath(); // Detener el movimiento actual
            }
        }

        // Continuar patrullando si llega al destino
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