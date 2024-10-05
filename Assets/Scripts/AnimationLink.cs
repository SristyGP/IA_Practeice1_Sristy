using UnityEngine;
using UnityEngine.AI;

public class AnimationLink : MonoBehaviour
{
    [SerializeField]
    private string m_AnimationSpeedParameterName;
    private Animator m_AnimatorComponent;
    private UnityEngine.AI.NavMeshAgent m_NavAgentComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
       m_AnimatorComponent = transform.GetChild(0).GetComponent<Animator>();
       m_NavAgentComponent = GetComponent<NavMeshAgent>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        m_AnimatorComponent.SetFloat(m_AnimationSpeedParameterName, m_NavAgentComponent.speed);
    }
    
}
