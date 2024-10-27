using UnityEngine;
using UnityEngine.AI;

public class HideBehaviour : StateMachineBehaviour
{
    private float hideDuration = 5f; // Tiempo en segundos que permanecerá en Hide
    private float hideTimer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hideTimer = 0f; // Reiniciar el temporizador al entrar en Hide
        animator.GetComponent<NavMeshAgent>().isStopped = true; // Detener al NPC
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hideTimer += Time.deltaTime; // Incrementar el temporizador

        if (hideTimer >= hideDuration)
        {
            animator.SetTrigger("ToSearch"); // Cambiar al estado Search después de esconderse
            animator.ResetTrigger("ThiefToFlee");
            animator.ResetTrigger("ToHide");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<NavMeshAgent>().isStopped = false; // Reactivar el movimiento del NPC al salir de Hide
    }
}
