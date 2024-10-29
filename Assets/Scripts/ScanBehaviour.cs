using UnityEngine;

public class ScanBehaviour : StateMachineBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform guard;
    private float detectionRange = 5f; // Distancia m�nima para huir
    private float fleeDistance = 10f; // Distancia a la que debe alejarse

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        guard = GameObject.FindWithTag("Guard").transform;
        SetFleeTarget(); // Establece el destino de huida inicial
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float distanceToGuard = Vector3.Distance(agent.transform.position, guard.position);

        if (distanceToGuard <= detectionRange)
        {
            SetFleeTarget(); // Cambia de direcci�n solo si el guardia est� cerca
        }
        else if (distanceToGuard > detectionRange + 1f) // Agrega margen para evitar fluctuaci�n entre estados
        {
            animator.SetBool("ToHide", false); // Cambiar al estado Hide
            animator.SetBool("ThiefToFlee", false);
            animator.SetBool("ToSearch", true);
        }
    }

    private void SetFleeTarget()
    {
        // Calcula la direcci�n de huida y establece el destino
        Vector3 fleeDirection = (agent.transform.position - guard.position).normalized;
        agent.SetDestination(agent.transform.position + fleeDirection * fleeDistance);
    }
}
