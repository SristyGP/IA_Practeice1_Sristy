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
        Debug.Log(guard);

        // Calcula y establece la direcci�n inicial de huida
        SetFleeDirection();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Si el Guard est� a menos de 5 metros, recalcula la direcci�n de huida
        if (Vector3.Distance(agent.transform.position, guard.position) <= detectionRange)
        {
            Debug.Log("Guardia detectado, cambiando de direcci�n");
            SetFleeDirection();
        }

        // Si se aleja m�s de la distancia de detecci�n, cambia al estado Search
        if (Vector3.Distance(agent.transform.position, guard.position) > detectionRange)
        {
            animator.SetTrigger("ToSearch"); // Cambia de estado al alejarse
            animator.ResetTrigger("ThiefToFlee");
            animator.ResetTrigger("ToHide");
        }
    }

    private void SetFleeDirection()
    {
        Debug.Log("cambio de dirraci�n");

        // Calcula la direcci�n y el destino de huida, y establece el destino en el NavMeshAgent
        Vector3 fleeDirection = (agent.transform.position - guard.position).normalized;
        Vector3 fleeTarget = agent.transform.position + fleeDirection * fleeDistance;
        agent.SetDestination(fleeTarget);
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
