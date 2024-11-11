using UnityEngine;
using UnityEngine.AI;

public class IsAlarmining : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform closestSafePoint;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = false;

        // Buscar el punto seguro más cercano utilizando el AiDirector
        closestSafePoint = FindClosestSafePoint();

        if (closestSafePoint != null)
        {
            // Configurar el destino del agente hacia el punto seguro
            agent.SetDestination(closestSafePoint.position);
        }
    }

    // Método para encontrar el punto seguro más cercano
    private Transform FindClosestSafePoint()
    {
        Transform closestPoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform safePoint in AiDirector.instance.safePoints)
        {
            float distance = Vector3.Distance(agent.transform.position, safePoint.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = safePoint;
            }
        }

        return closestPoint;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Detener el agente cuando salga del estado de alarma
        agent.isStopped = true;
    }
}
