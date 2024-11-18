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
        if (targetPosition != Vector3.zero) // Verifica que las coordenadas sean v�lidas
        {
            agent.SetDestination(targetPosition);
            Debug.Log("Guardia dirigi�ndose a la posici�n reportada del Thief: " + targetPosition);
        }
        else
        {
            Debug.LogWarning("No se recibi� una posici�n v�lida para el Thief.");
        }

        // Asegurar que otros estados est�n desactivados
        animator.SetBool("ToPatrol", false);
        animator.SetBool("ToPursue", false);
        animator.SetBool("ToScan", false);
        


        Debug.Log("Guardia bloqueado en estado de alarma.");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Mant�n al guardia en estado de alarma mientras sea necesario
        if (!AiDirector.instance.isAlarmActive)
        {
            Debug.Log("La alarma se ha desactivado. Regresando al estado de patrulla.");
            animator.SetBool("ToPatrol", true); // Transici�n de regreso a patrulla
            animator.SetBool("ToAlarmGuard", false); // Sale del estado de alarma
        }
    }
}