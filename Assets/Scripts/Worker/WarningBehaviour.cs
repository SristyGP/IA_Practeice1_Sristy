using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class WarningBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform switchPoint; // Punto de interruptor m�s cercano
    private bool alarmActivated = false; // Asegura que la alarma solo se active una vez

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
        // Verifica si ha llegado al interruptor y a�n no ha activado la alarma
        if (!alarmActivated && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            //AiDirector.instance.TriggerAlarm(animator.transform.position); // Notifica la posici�n del Interruptor
            alarmActivated = true; // Evita activar la alarma m�ltiples veces

           
        }
    }
}