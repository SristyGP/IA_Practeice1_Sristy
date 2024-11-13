using UnityEngine;
using UnityEngine.AI;

public class WarningBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform switchPoint; // Punto de interruptor más cercano

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();

        // Obtén el punto de interruptor más cercano desde AiDirector
        switchPoint = AiDirector.instance.GetClosestSwitchPoint(animator.transform.position);

        // Muévete hacia el interruptor si existe
        if (switchPoint != null)
        {
            agent.SetDestination(switchPoint.position);
        }
        else
        {
            Debug.LogWarning("No se encontró ningún interruptor cercano para el Worker.");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Si ha llegado al interruptor, activa la alarma
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            AiDirector.instance.TriggerAlarm(animator.transform.position); // Notifica la posición del Thief
            animator.SetBool("ToAlarm", true); // Cambia al estado de alarma en la máquina de estados
        }
    }
}