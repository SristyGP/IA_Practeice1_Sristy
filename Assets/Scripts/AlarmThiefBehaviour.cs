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

            // Aquí activas la transición para que vuelva al estado "Search"
            animator.SetBool("ToSearch", true);  // Cambia al estado de búsqueda cuando llega al punto seguro
        }
    }

    // Método para obtener el punto seguro más cercano
}
