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
    private Vector3 rayOrigin;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        var Agent = animator.GetComponent<Agent>(); 

        waypoints = Agent.waypoints;
        thief = GameObject.FindWithTag("Thief").transform;

        
        lastWaypointIndex = Agent.currentWaypointIndex;
        agent.SetDestination(waypoints[lastWaypointIndex].position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 5, Color.red); // Raycast visible (flecha roja)
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0); // Posicionamiento de raycast - 1 metro es deamsiado alto

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("Huida completada, volviendo a patrullaje");
            animator.ResetTrigger("ToFlee"); // Trigger o SetBool("nombre ej asustado", true)
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