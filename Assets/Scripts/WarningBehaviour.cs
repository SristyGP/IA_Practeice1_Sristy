using UnityEngine;
using UnityEngine.AI;

public class WarningBehaviour : StateMachineBehaviour
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
        // Cuando llega al punto seguro, activa la alarma y pasa a Affraid
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Activa la alarma en el AiDirector
            AiDirector.instance.ActivateAlarm(thief.position);

            // Cambia al estado de miedo
            animator.SetBool("ToFlee", false);
            animator.SetBool("ToPosing", false);
            animator.SetBool("ToAffraid", true);
        }
    }
}