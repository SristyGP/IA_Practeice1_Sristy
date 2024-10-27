using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class FleeWorkerbehaviour : StateMachineBehaviour
{
   
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private Transform guard;
    private Transform thief;
    private int lastWaypointIndex;
    private float detectionRange = 10f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        var Agent = animator.GetComponent<Agent>(); 

        waypoints = Agent.waypoints;
        guard = GameObject.FindWithTag("Guard").transform;
        thief = GameObject.FindWithTag("Thief").transform;

        // Mueve al último punto seguro
        lastWaypointIndex = Agent.currentWaypointIndex;
        agent.SetDestination(waypoints[lastWaypointIndex].position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Cuando llega al punto seguro, pasa a Affraid
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            animator.ResetTrigger("ToFlee");
            animator.ResetTrigger("ToPosing");
            animator.SetTrigger("ToAffraid");
        }
    }
}