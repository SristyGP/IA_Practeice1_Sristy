using UnityEngine;
using UnityEngine.AI;

public class AlarmThiefBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform[] safePoints; // Puntos seguros
    private Transform closestSafePoint; // El punto seguro más cercano

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();

        // Obtener los puntos seguros desde el AiDirector
        safePoints = AiDirector.instance.safePoints.ToArray();  // Asegúrate de que AiDirector tiene asignados los puntos seguros

        if (safePoints.Length == 0)
        {
            Debug.LogWarning("No hay puntos seguros disponibles.");
            return;
        }

        // Encuentra el punto seguro más cercano
        closestSafePoint = GetClosestSafePoint(animator.transform.position);

        // Mueve al punto seguro más cercano
        if (closestSafePoint != null)
        {
            agent.SetDestination(closestSafePoint.position);
        }
        else
        {
            Debug.LogWarning("No se pudo encontrar un punto seguro cercano.");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Verifica si el Thief ha llegado al punto seguro
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Debug.Log("Thief ha llegado al punto seguro.");

            // Aquí activas la transición para que vuelva al estado "Search"
            animator.SetBool("ToSearch", true);  // Cambia al estado de búsqueda cuando llega al punto seguro
        }
    }

    // Método para obtener el punto seguro más cercano
    private Transform GetClosestSafePoint(Vector3 currentPosition)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform safePoint in safePoints)
        {
            float distance = Vector3.Distance(currentPosition, safePoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = safePoint;
            }
        }

        return closest;
    }
}
