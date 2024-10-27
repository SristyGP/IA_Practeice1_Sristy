using UnityEngine;
using UnityEngine.AI;

public class AffraidBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform guard;
    private float detectionRange = 5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        guard = GameObject.FindWithTag("Guard").transform;

        // Detiene al worker
        agent.isStopped = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Activa el estado Posing solo si detecta al guardia
        if (Vector3.Distance(agent.transform.position, guard.position) <= detectionRange)
        {
            agent.isStopped = false;
            animator.SetTrigger("ToPosing");
            animator.ResetTrigger("ToAffraid");
            animator.ResetTrigger("ToFlee");
        }
    }
}