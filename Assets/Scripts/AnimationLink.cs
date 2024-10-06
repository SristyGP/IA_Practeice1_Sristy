using UnityEngine;
using UnityEngine.AI;

public class AnimationLink : MonoBehaviour
{
    [SerializeField]
    private string m_AnimationSpeedParameterName;
    private Animator m_AnimatorComponent;
    private UnityEngine.AI.NavMeshAgent m_NavAgentComponent;
    private float originalSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
       m_AnimatorComponent = transform.GetChild(0).GetComponent<Animator>();
       m_NavAgentComponent = GetComponent<NavMeshAgent>();
        originalSpeed = m_NavAgentComponent.speed;

    }
    

    // Update is called once per frame
    private void Update()
    {
        m_AnimatorComponent.SetFloat(m_AnimationSpeedParameterName, m_NavAgentComponent.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Maintenance") && gameObject.name == "Thief")
        {
            m_NavAgentComponent.speed = originalSpeed * 0.5f;  // Reducir la velocidad en un 50%
        }
    }

    // Detectar cuando el agente sale de la zona "Maintenance"
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Maintenance") && gameObject.name == "Thief")
        {
            m_NavAgentComponent.speed = originalSpeed;  // Restaurar la velocidad original
        }
    }

}
