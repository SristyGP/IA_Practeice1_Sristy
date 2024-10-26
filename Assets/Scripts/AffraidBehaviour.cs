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
        agent = animator.GetComponent<NavMeshAgent>();
        guard = GameObject.FindWithTag("Guard").transform;

        // Detiene el movimiento del agente al entrar en el estado
        agent.isStopped = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 5, Color.red); // Raycast visible (flecha roja)
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0); // Posicionamiento de raycast - 1 metro es deamsiado alto

        // Verifica si el guardia está dentro del rango de detección
        if (Vector3.Distance(agent.transform.position, guard.position) <= detectionRange)
        {
            Debug.Log("Guard detectado dentro del rango, regresando a la ruta");

            // Establece la ruta de regreso al último waypoint
            agent.isStopped = false; // Permite el movimiento
            animator.SetTrigger("ToPatrol"); // Cambia al estado de patrullaje
        }
    }
}