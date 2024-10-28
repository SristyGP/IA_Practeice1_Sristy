using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PursueBehaviour : StateMachineBehaviour
{
    public float FollowDistance = 2f;
    public Transform Target; // Cambiar a Vector3
    private NavMeshAgent m_AgentComponent;
    private NavMeshAgent agent;
    private Vector3 rayOrigin;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Target = animator.GetComponent<Agent>().target; // Asignando el Vector3 directamente
        m_AgentComponent = animator.GetComponent<NavMeshAgent>();
        m_AgentComponent.autoBraking = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_AgentComponent.destination = Target.position; // Asigna directamente la posición (Vector3)
        

        // Calcular la distancia al objetivo
        float distanceToTarget = Vector3.Distance(animator.transform.position, Target.position);

        if (distanceToTarget > FollowDistance)
        {
           // Debug.Log("funciona");
            animator.SetBool("ToPatrol", false);
            
        }
        else
        {
           // Debug.Log("no funciona");
        }

        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0);
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 10, Color.red);

        RaycastHit hitclose;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hitclose, 3f))
        {


            if (hitclose.collider.gameObject.CompareTag("Thief"))
            {
                Debug.Log("cambia a estado Attack");
                animator.SetBool("ToAttack", true); // Cambiar al estado Attack
                animator.SetBool("ToPursue", false);
                animator.SetBool("ToSeek", false);
                animator.SetBool("ToPatrol", false);

                agent.ResetPath(); // Detener el movimiento hacia el waypoint

            }
        }
    }
}
