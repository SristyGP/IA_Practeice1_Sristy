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
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("Thief ha llegado al punto seguro.");

            // Verifica si la alarma está desactivada
            if (!AiDirector.instance.isAlarmActive)
            {
                Debug.Log("La alarma está desactivada. Cambiando al estado 'Search'.");
                animator.SetBool("ToSearch", true);  // Cambia al estado de búsqueda
                animator.SetBool("ToAlarm", false);  // Asegúrate de desactivar el estado de alarma
            }
            else
            {
                Debug.Log("La alarma sigue activa. Permaneciendo en el punto seguro.");
            }
        }

    // Método para obtener el punto seguro más cercano
    }
}

