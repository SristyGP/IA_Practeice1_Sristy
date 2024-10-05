using UnityEngine;
using UnityEngine.AI;

public class AlertBehaviour : StateMachineBehaviour
{
    public float FollowDistance = 2f;
    public Vector3 Target; // Cambiar a Vector3
    public NavMeshAgent m_AgentComponent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Target = animator.GetComponent<Agent>().target; // Asignando el Vector3 directamente
        m_AgentComponent = animator.GetComponent<NavMeshAgent>();
        m_AgentComponent.autoBraking = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_AgentComponent.destination = Target; // Asigna directamente la posici�n (Vector3)
        if (Vector3.Distance(animator.transform.position, Target) > FollowDistance)
        {
            animator.SetBool("Alert", false);
        }
    }
}
