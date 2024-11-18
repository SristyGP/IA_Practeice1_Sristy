using UnityEngine;
using UnityEngine.AI;

public class AlarmGuardBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();

        // Obtener las coordenadas del AI Director
        Vector3 targetPosition = AiDirector.instance.thiefReportedPosition;
        // Asignar el destino al guardia
        if (targetPosition != Vector3.zero) // Verifica que las coordenadas sean válidas
        {
            agent.SetDestination(targetPosition);
            Debug.Log("Guardia dirigiéndose a la posición reportada del Thief: " + targetPosition);
        }
        else
        {
            Debug.LogWarning("No se recibió una posición válida para el Thief.");
        }

        // Asegurar que otros estados estén desactivados
        animator.SetBool("ToPatrol", false);
        animator.SetBool("ToPursue", false);
        animator.SetBool("ToScan", false);
        


        Debug.Log("Guardia bloqueado en estado de alarma.");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Mantén al guardia en estado de alarma mientras sea necesario
        if (!AiDirector.instance.isAlarmActive)
        {
            Debug.Log("La alarma se ha desactivado. Regresando al estado de patrulla.");
            animator.SetBool("ToPatrol", true); // Transición de regreso a patrulla
            animator.SetBool("ToAlarmGuard", false); // Sale del estado de alarma
        }
    }
}