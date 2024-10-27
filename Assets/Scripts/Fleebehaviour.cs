using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Fleebehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private Transform guard;
    private Transform thief;
    private int lastWaypointIndex;
    private float detectionRange = 5f;
    private Vector3 rayOrigin;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        var Agent = animator.GetComponent<Agent>();

        waypoints = Agent.waypoints;
        guard = GameObject.FindWithTag("Guard").transform;
        thief = GameObject.FindWithTag("Thief").transform;

        lastWaypointIndex = Agent.currentWaypointIndex;
        agent.SetDestination(waypoints[lastWaypointIndex].position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 5, Color.red);
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            if (hit.collider.gameObject.name == "Guard")
            {
                // Cambia exclusivamente al estado Affraid
                animator.ResetTrigger("ToPosing");
                animator.ResetTrigger("ToFlee");
                animator.SetTrigger("ToAffraid");
            }
        }

        if (Vector3.Distance(agent.transform.position, guard.position) <= detectionRange)
        {
            agent.SetDestination(waypoints[lastWaypointIndex].position);
        }
    }
}