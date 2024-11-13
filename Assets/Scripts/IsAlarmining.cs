using UnityEngine;
using UnityEngine.AI;

public class IsAlarmining : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform Puntodeagrupamiento; // Punto de agrupamiento

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();

        // Obt�n el punto de agrupamiento desde el AI Director
        Puntodeagrupamiento = AiDirector.instance.GetPuntodeagrupamiento();

        // Mu�vete hacia el punto de agrupamiento
        if (Puntodeagrupamiento != null)
        {
            agent.SetDestination(Puntodeagrupamiento.position);
        }
        else
        {
            Debug.LogWarning("No se encontr� un punto de agrupamiento.");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Verifica si el `Worker` ha llegado al punto de agrupamiento
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("Worker ha llegado al punto de agrupamiento.");
            // Aqu� podr�as a�adir cualquier comportamiento adicional si es necesario
        }
    }
}
