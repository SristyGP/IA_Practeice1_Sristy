using UnityEngine;
using UnityEngine.AI;

public class HideBehaviour : StateMachineBehaviour
{
    private float hideDuration = 5f; // Tiempo en segundos que permanecer� en Hide
    private float hideTimer;
    private NavMeshAgent agent;
    private Vector3 maintenancePoint; // Punto objetivo en Maintenance

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hideTimer = 0f; // Reiniciar el temporizador al entrar en Hide
        agent = animator.GetComponent<NavMeshAgent>();

        // Obtener el ducto m�s cercano en Maintenance
        NavMeshHit hit;
        if (NavMesh.SamplePosition(animator.transform.position, out hit, 20f, 1 << NavMesh.GetAreaFromName("Maintenance"))) // Alcance incrementado
        {
            maintenancePoint = hit.position;
            agent.SetDestination(maintenancePoint); // Se dirige directamente al ducto
            agent.isStopped = false;
        }
        else
        {
            Debug.LogWarning("No se encontr� un punto en la zona de Maintenance cercano.");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Obtener el ducto m�s cercano en Maintenance
        NavMeshHit hit;
        if (NavMesh.SamplePosition(animator.transform.position, out hit, 20f, 1 << NavMesh.GetAreaFromName("Maintenance"))) // Alcance incrementado
        {
            maintenancePoint = hit.position;
            agent.SetDestination(maintenancePoint); // Se dirige directamente al ducto
            agent.isStopped = false;
        }
        else
        {
            Debug.LogWarning("No se encontr� un punto en la zona de Maintenance cercano.");
        }
        // Verifica si ha llegado al ducto en Maintenance
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true; // Detiene el movimiento al llegar al ducto
            hideTimer += Time.deltaTime; // Inicia el temporizador

            if (hideTimer >= hideDuration)
            {
                animator.SetBool("ToHide", false); // Cambiar al estado Hide
                animator.SetBool("ThiefToFlee", false);
                animator.SetBool("ToSearch", true);
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = false; // Reactivar el movimiento del NPC al salir de Hide
    }
}
