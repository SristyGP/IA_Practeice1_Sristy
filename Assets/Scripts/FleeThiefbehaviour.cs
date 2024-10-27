using UnityEngine;
using UnityEngine.AI;

public class FleeThiefbehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform guard;
    private float detectionRange = 5f; // Distancia m�nima para huir
    private float fleeDistance = 10f; // Distancia a la que debe alejarse

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
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
            animator.SetTrigger("ToSearch"); // Cambia a estado de b�squeda
        }
    }

    private void SetFleeTarget()
    {
        // Calcula la direcci�n de huida y establece el destino
        Vector3 fleeDirection = (agent.transform.position - guard.position).normalized;
        agent.SetDestination(agent.transform.position + fleeDirection * fleeDistance);
    }

    //// Detecta colisiones para arrastrar el obst�culo
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Obstacle") && Vector3.Distance(agent.transform.position, collision.transform.position) < 1f)
    //    {
    //        Rigidbody obstacleRb = collision.gameObject.GetComponent<Rigidbody>();
    //        if (obstacleRb != null)
    //        {
    //            Vector3 pushDirection = collision.transform.position - agent.transform.position;
    //            obstacleRb.AddForce(pushDirection.normalized * 5f, ForceMode.Impulse); // Aplica fuerza al obst�culo
    //            isDraggingObstacle = true;
    //        }
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Obstacle"))
    //    {
    //        isDraggingObstacle = false; // Deja de arrastrar cuando se separa del obst�culo
    //    }
    //}
}
