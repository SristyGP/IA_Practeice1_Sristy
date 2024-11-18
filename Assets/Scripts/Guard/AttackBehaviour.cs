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

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 10f))
        {


            if (hit.collider.gameObject.CompareTag("Thief"))
            {


                agent.ResetPath(); // Detener el movimiento hacia el waypoint

            }
            else
            {
                Debug.Log("cambia a estado Seek 1");
                animator.SetBool("ToPursue", false); // Cambiar al estado Attack
                animator.SetBool("ToPatrol", false);
                animator.SetBool("ToSeek", true);
            }
        }
        else
        {
            Debug.Log("cambia a estado Seek 2");
            animator.SetBool("ToPursue", false); // Cambiar al estado Attack
            animator.SetBool("ToPatrol", false);
            animator.SetBool("ToSeek", true);
        }

        RaycastHit hitclose;
        if (Physics.Raycast(rayOrigin, animator.transform.forward, out hitclose, 2f))
        {
            // Verifica si el Thief está en rango de ataque (2 metros o menos)
            if (hitclose.collider.CompareTag("Thief"))
            {
                // Cambia a Pursue si el Thief está a más de 2 metros
                if (Vector3.Distance(animator.transform.position, hitclose.transform.position) > 5f)
                {
                    Debug.Log("Thief fuera de alcance de ataque, cambiando a Pursue");
                    animator.SetBool("ToPatrol", false);
                    animator.SetBool("ToAttack", false); // Desactiva el estado Attack
                    animator.SetBool("ToPursue", true); // Cambiar al estado Pursue
                }
                else
                {
                    hitclose.collider.gameObject.GetComponent<Animator>().SetBool("KO", true);
                }


               
            }
        }
    }
}
