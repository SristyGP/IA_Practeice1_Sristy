using UnityEngine;
using UnityEngine.AI;

public class AlarmThiefBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(AiDirector.instance.GetClosestSafePoint(animator.transform.position).position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Verifica si el Thief ha llegado al punto seguro
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("Thief ha llegado al punto seguro.");

            // Aqu� activas la transici�n para que vuelva al estado "Search"
            animator.SetBool("ToSearch", true);  // Cambia al estado de b�squeda cuando llega al punto seguro
        }
    }

    // M�todo para obtener el punto seguro m�s cercano
}
