using UnityEngine;
using UnityEngine.AI;

public class AlarmGuardBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Vector3 rayOrigin;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();

        // Obtener las coordenadas del AI Director
        Vector3 targetPosition = AiDirector.instance.thiefReportedPosition;
        // Asignar el destino al guardia
        if (targetPosition != Vector3.zero) // Verifica que las coordenadas sean válidas
        {
            agent.SetDestination(targetPosition);
            Debug.Log("Guardia dirigiéndose a la posición reportada del Thief: " + targetPosition);
        }
        else
        {
            Debug.LogWarning("No se recibió una posición válida para el Thief.");
        }

        // Asegurar que otros estados estén desactivados
        animator.SetBool("ToPatrol", false);
        animator.SetBool("ToPursue", false);
        animator.SetBool("ToScan", false);




        Debug.Log("Guardia bloqueado en estado de alarma.");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rayOrigin = animator.transform.position + new Vector3(0, 0.5f, 0);
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 10, Color.red);
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            animator.SetBool("ToScan", true);

        }
        RaycastHit hitclose;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hitclose, 5f))
        {


            if (hitclose.collider.gameObject.CompareTag("Thief"))
            {
                Debug.Log("cambia a estado Pursue");

                animator.SetBool("ToPursue", true); // Cambiar al estado Attack


                agent.ResetPath(); // Detener el movimiento hacia el waypoint

            }
            else
            {

            }
        }
    }
}