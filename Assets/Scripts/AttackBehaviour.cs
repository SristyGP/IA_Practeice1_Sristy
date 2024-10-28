using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 rayOrigin;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("se detiene el agente");
        // Obtiene el agente NavMesh y lo detiene
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.isStopped = true; // Detiene el movimiento del agente
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Configura la posición inicial del Raycast
        rayOrigin = animator.transform.position + new Vector3(0, 0.1f, 0);
        Debug.DrawRay(rayOrigin, animator.transform.forward * 10, Color.red);

        RaycastHit hitclose;
        if (Physics.Raycast(rayOrigin, animator.transform.forward, out hitclose, 5f))
        {
            // Verifica si el Thief está en rango de ataque (2 metros o menos)
            if (hitclose.collider.CompareTag("Thief"))
            {
                // Cambia a Pursue si el Thief está a más de 2 metros
                if (Vector3.Distance(animator.transform.position, hitclose.transform.position) > 5f)
                {
                    Debug.Log("Thief fuera de alcance de ataque, cambiando a Pursue");
                    animator.SetBool("ToPursue", true); // Cambiar al estado Pursue
                    animator.SetBool("ToAttack", false); // Desactiva el estado Attack
                }


                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 10f))
                {


                    if (hitclose.collider.gameObject.CompareTag("Thief"))
                    {
                        Debug.Log("cambia a estado Seek");
                        animator.SetBool("ToPursue", false); // Cambiar al estado Attack
                        animator.SetBool("ToSeek", true);
                        animator.SetBool("ToPatrol", false);

                        agent.ResetPath(); // Detener el movimiento hacia el waypoint

                    }
                }
            }
        }
    }
}
