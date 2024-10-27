using UnityEngine;
using UnityEngine.AI;

public class FleeThiefbehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform guard;
    private Transform worker;
    private float detectionRange = 10f; // Distancia mínima para huir
    private float fleeDistance = 10f; // Distancia a la que debe alejarse
    private Vector3 fleeDirection;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        guard = GameObject.FindWithTag("Guard").transform;
        worker = GameObject.FindWithTag("Worker").transform;

        // Calcula la dirección de huida
        fleeDirection = (agent.transform.position - guard.position).normalized; // Dirección opuesta al guard
        Vector3 fleeTarget = agent.transform.position + fleeDirection * fleeDistance;

        // Establece el destino del agente
        agent.SetDestination(fleeTarget);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Verifica la distancia al guardia
        if (Vector3.Distance(agent.transform.position, guard.position) > detectionRange)
        {
            animator.SetTrigger("ThiefToFlee");
            return; // Sale si se aleja más de la distancia de detección
        }

        // Asegúrate de que el agente siga huyendo mientras esté dentro del rango
        fleeDirection = (agent.transform.position - guard.position).normalized; // Recalcula la dirección de huida
        Vector3 fleeTarget = agent.transform.position + fleeDirection * fleeDistance;
        agent.SetDestination(fleeTarget);
    }
}
