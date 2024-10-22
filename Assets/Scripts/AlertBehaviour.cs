using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class AlertBehaviour : StateMachineBehaviour
{
    public float FollowDistance = 2f;
    public Transform Target; // Cambiar a Vector3
    private NavMeshAgent m_AgentComponent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Target = animator.GetComponent<Agent>().target; // Asignando el Vector3 directamente
        m_AgentComponent = animator.GetComponent<NavMeshAgent>();
        m_AgentComponent.autoBraking = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_AgentComponent.destination = Target.position; // Asigna directamente la posición (Vector3)
        //if (Physics.Raycast().Distance(animator.transform.position, Target.position) > FollowDistance)
        //{
        //    Debug.Log("funciona");
        //    animator.SetBool("Alert", false);
        //}
        //else
        //{
        //    Debug.Log("no funciona");
        //}
    }
}
