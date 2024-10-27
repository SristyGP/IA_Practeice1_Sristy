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
        Debug.Log("Entrando en estado Affraid");
        agent = animator.GetComponent<NavMeshAgent>();
        guard = GameObject.FindWithTag("Guard").transform;

        // Detiene el movimiento del agente al entrar en el estado Affraid
        agent.isStopped = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 5, Color.red); // Raycast visible (flecha roja)
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0); // Posicionamiento de raycast - 1 metro es deamsiado alto

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            //Debug.Log("detección");
            if (hit.collider.gameObject.name == "Guard")
            {
                //Debug.Log("Thief detectado, activando huida");
                animator.SetTrigger("ToPosing");
            }
        }

        // Verifica si el guardia está dentro del rango de detección
        if (Vector3.Distance(agent.transform.position, guard.position) <= detectionRange)
        {
            Debug.Log("Guardia detectado, cambiando a estado Posing");
            agent.isStopped = false;
            animator.ResetTrigger("ToAffraid"); // Limpia el trigger anterior
            animator.SetTrigger("ToPosing");    // Cambia a estado Posing
        }
    }
}