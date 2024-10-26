using UnityEngine;
using UnityEngine.AI;

public class AffraidBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform guard;
    private float detectionRange = 5f;
    private Vector3 rayOrigin;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("afrraid");
        agent = animator.GetComponent<NavMeshAgent>();
        guard = GameObject.FindWithTag("Guard").transform;

        // Detiene el movimiento del agente al entrar en el estado
        agent.isStopped = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Actualizar rayOrigin antes de dibujar el rayo
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0); // Posicionamiento del raycast
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 5, Color.red); // Raycast visible (flecha roja)

        // Verifica si el guardia está dentro del rango de detección
        if (Vector3.Distance(agent.transform.position, guard.position) <= detectionRange)
        {
            Debug.Log("Guard detectado dentro del rango, regresando a la ruta");

            // Permite el movimiento y cambia a patrullaje
            agent.isStopped = false; // Permite el movimiento
            animator.SetTrigger("asustado"); // Cambia al estado de patrullaje
            return; // Salimos para evitar la siguiente comprobación
        }

        // Si el worker no detecta al guardia, se queda quieto y espera
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("Huida completada, volviendo a patrullaje");
            animator.ResetTrigger("ToAffraid"); // Reinicia el trigger si es necesario
            animator.SetTrigger("ToFlee"); // Cambia al estado de huida
        }
    }
}