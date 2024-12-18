using UnityEngine;
using UnityEngine.AI;


public class PosingBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    int waypointIndex;
    public Transform target;
    private Vector3 rayOrigin;
    private bool alarmActivated = false; // Asegura que la alarma solo se active una vez

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("comienza el estado de flee");
        agent = animator.GetComponent<NavMeshAgent>();
        
        UpdateDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 10, Color.red); // Raycast visible (flecha roja)
        rayOrigin = animator.transform.position + new Vector3(0, 0.5f, 0); // Posicionamiento de raycast - 1 metro es deamsiado alto

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            if (hit.collider.gameObject.CompareTag("Thief"))
            {
                Debug.Log("Thief detectado, activando huida");


                // Obtener las coordenadas del Thief detectado
                Vector3 thiefPosition = hit.collider.transform.position;
                Debug.Log("Comunicar las coordenadas al AiDirector");
                AiDirector.instance.ReportThiefPosition(hit.collider.transform.position);

                animator.SetBool("ToPosing", false);
                animator.SetBool("ToAffraid", false);
                animator.SetBool("ToWarning", true);
            }

         
        }

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            GoToNextWaypoint();
        }
    }

   

    void UpdateDestination()
    {
        target = AiDirector.instance.Workerwaypoints[waypointIndex];
        agent.SetDestination(target.position);
    }

    void GoToNextWaypoint()
    {
        
        waypointIndex = (waypointIndex + 1) % AiDirector.instance.Workerwaypoints.Length;
        UpdateDestination();
    } 
}
