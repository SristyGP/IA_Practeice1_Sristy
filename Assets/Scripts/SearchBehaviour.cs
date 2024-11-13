using UnityEngine;
using UnityEngine.AI;

public class SearchBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;  // Puntos de patrullaje
    private int waypointIndex;
    private Vector3 rayOrigin;

    // Puntos seguros a los que puede ir el Thief cuando la alarma se activa
    private Transform[] safePoints;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = false;

        // Asignar los puntos de patrullaje y los puntos seguros desde el AiDirector
        waypoints = AiDirector.instance.Thiefwaypoints;
        safePoints = AiDirector.instance.safePoints.ToArray();

        UpdateDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Si la alarma está activada, dirigir al Thief al punto seguro más cercano
        if (animator.GetBool("AlarmActive"))
        {
            Transform closestSafePoint = GetClosestSafePoint(animator.transform.position);
            if (closestSafePoint != null)
            {
                agent.SetDestination(closestSafePoint.position);
            }
        }
        else
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
                    animator.SetBool("ThiefToFlee", false);
                    animator.SetBool("ToSearch", false);
                    animator.SetBool("KO", false);
                    animator.SetBool("ToHide", true); // Cambiar al estado Hide
                    agent.ResetPath(); // Detener el movimiento hacia el waypoint
                }
                else if (hit.collider.gameObject.CompareTag("Guard"))
                {
                    animator.SetBool("ToHide", false);
                    animator.SetBool("ThiefToFlee", true);
                    animator.SetBool("ToSearch", false);
                    animator.SetBool("KO", false);
                    agent.ResetPath(); // Detener el movimiento hacia el waypoint
                }
            }
        }

        // Si ha alcanzado el destino, se mueve al siguiente waypoint
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            GoToNextWaypoint();
        }
    }

    // Método para obtener el punto seguro más cercano
    private Transform GetClosestSafePoint(Vector3 currentPosition)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform safePoint in safePoints)
        {
            float distance = Vector3.Distance(currentPosition, safePoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = safePoint;
            }
        }

        return closest;
    }

    // Actualiza el destino al siguiente waypoint
    void UpdateDestination()
    {
        agent.SetDestination(waypoints[waypointIndex].position);
    }

    void GoToNextWaypoint()
    {
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
        UpdateDestination();
    }
}