using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Fleebehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private Transform thief;
    private int lastWaypointIndex;
    private float detectionRange = 5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        var Agent = animator.GetComponent<Agent>(); // Referencia a tu script `Agent` 

        waypoints = Agent.waypoints;
        thief = GameObject.FindWithTag("Thief").transform;

        // Guardar el último waypoint alcanzado y dirigirse a él
        lastWaypointIndex = Agent.currentWaypointIndex;
        agent.SetDestination(waypoints[lastWaypointIndex].position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("Huida completada, volviendo a patrullaje");
            animator.ResetTrigger("ToFlee");
            animator.SetTrigger("ToPatrol");
            return;
        }

        if (Vector3.Distance(agent.transform.position, thief.position) <= detectionRange)
        {
            Debug.Log("Thief detectado dentro del rango de huida");
            agent.SetDestination(waypoints[lastWaypointIndex].position);
        }
    }
}