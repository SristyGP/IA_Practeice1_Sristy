using UnityEngine;

public class ScanBehaviour : StateMachineBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform guard;
    private float detectionRange = 5f; // Distancia m�nima para huir
    private float fleeDistance = 10f; // Distancia a la que debe alejarse
    private Vector3 rayOrigin;
    public float rotationSpeed = 90f; // Velocidad de rotación en grados por segundo
    private bool isRotating = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        guard = GameObject.FindWithTag("Guard").transform;
       
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Si la rotación está habilitada
        if (isRotating)
        {
            // Rota el objeto alrededor del eje Y (puedes cambiar el eje si lo necesitas)
            animator.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            // Si ha completado 360 grados, detiene la rotación
            if (animator.transform.eulerAngles.y >= 360f)
            {
                isRotating = false;
                animator.transform.eulerAngles = new Vector3(animator.transform.eulerAngles.x, 360f, animator.transform.eulerAngles.z);
                animator.SetBool("ToAttack", false);
                animator.SetBool("ToSeek", false);
                animator.SetBool("ToPatrol", true);
                // Asegura que se detenga exactamente en 360 grados
            }
        }

        Debug.DrawRay(rayOrigin, animator.transform.TransformDirection(Vector3.forward) * 10, Color.red); // Raycast visible (flecha roja)
        rayOrigin = animator.transform.position + new Vector3(0, 0.5f, 0); // Posicionamiento de raycast - 1 metro es deamsiado alto

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, animator.transform.TransformDirection(Vector3.forward), out hit, 10f))
        {
            //Debug.Log("detección");
            if (hit.collider.gameObject.CompareTag("Thief"))
            {
                //Debug.Log("Thief detectado, activando huida");
                animator.SetBool("ToAttack", false);
                animator.SetBool("ToSeek", false);
                animator.SetBool("ToPursue", true);

            }
            else
            {
                //animator.SetBool("ToAttack", false);
                //animator.SetBool("ToSeek", false);
                //animator.SetBool("ToPatrol", true);
            }
        }
        else
        {
            //animator.SetBool("ToAttack", false);
            //animator.SetBool("ToSeek", false);
            //animator.SetBool("ToPatrol", true);

        }

        
    }


}
