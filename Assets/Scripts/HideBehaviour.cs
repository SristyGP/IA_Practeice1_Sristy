using UnityEngine;
using UnityEngine.AI;

public class HideBehaviour : StateMachineBehaviour
{
    private float hideDuration = 5f; // Tiempo en segundos que permanecerá en Hide
    private float hideTimer;
    private NavMeshAgent agent;
    private Vector3 maintenancePoint; // Punto objetivo en Maintenance

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hideTimer = 0f; // Reiniciar el temporizador al entrar en Hide
        agent = animator.GetComponent<NavMeshAgent>();

        // Obtener el ducto más cercano en Maintenance
        NavMeshHit hit;
        if (NavMesh.SamplePosition(animator.transform.position, out hit, 20f, 1 << NavMesh.GetAreaFromName("Maintenance")))
        {
            maintenancePoint = hit.position;
            agent.SetDestination(maintenancePoint);
            agent.isStopped = false;
        }
        else
        {
            Debug.LogWarning("No se encontró un punto en la zona de Maintenance cercano.");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Verifica si ha llegado al ducto en Maintenance
        if (Vector3.Distance(animator.transform.position, maintenancePoint) <= agent.stoppingDistance && agent.remainingDistance <= agent.stoppingDistance)
        {
            hideTimer += Time.deltaTime; // Inicia el temporizador una vez que llega

            if (hideTimer >= hideDuration)
            {
                animator.SetTrigger("ToSearch"); // Cambiar al estado Search después de esconderse
                animator.ResetTrigger("ThiefToFlee");
                animator.ResetTrigger("ToHide");
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = false; // Reactivar el movimiento del NPC al salir de Hide
    }
}
