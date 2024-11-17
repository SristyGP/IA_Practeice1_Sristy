using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class WarningBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform switchPoint; // Punto de interruptor más cercano
    private bool alarmActivated = false; // Asegura que la alarma solo se active una vez

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
        // Verifica si ha llegado al interruptor y aún no ha activado la alarma
        if (!alarmActivated && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            //AiDirector.instance.TriggerAlarm(animator.transform.position); // Notifica la posición del Interruptor
            alarmActivated = true; // Evita activar la alarma múltiples veces

           
        }
    }
}