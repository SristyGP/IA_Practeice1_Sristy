using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("se detiene el agente");
        // Obtiene el agente NavMesh y lo detiene
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.isStopped = true; // Detiene el movimiento del agente
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Asegura que el agente se mantenga inactivo durante el estado KO
        if (agent != null && !agent.isStopped)
        {
            agent.isStopped = true;
        }
    }
}
