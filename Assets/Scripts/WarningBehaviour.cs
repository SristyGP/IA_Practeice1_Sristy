using UnityEngine;
using UnityEngine.AI;

public class WarningBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform switchPoint; // Punto de interruptor m�s cercano

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();

        // Obt�n el punto de interruptor m�s cercano desde AiDirector
        switchPoint = AiDirector.instance.GetClosestSwitchPoint(animator.transform.position);

        // Mu�vete hacia el interruptor si existe
        if (switchPoint != null)
        {
            agent.SetDestination(switchPoint.position);
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n interruptor cercano para el Worker.");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Si ha llegado al interruptor, activa la alarma
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            AiDirector.instance.TriggerAlarm(animator.transform.position); // Notifica la posici�n del Thief
            animator.SetBool("ToAlarm", true); // Cambia al estado de alarma en la m�quina de estados
        }
    }
}