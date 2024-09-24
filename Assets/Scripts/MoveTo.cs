using UnityEngine;

public class MoveTo : MonoBehaviour
{

    public Transform Goal;
    private UnityEngine.AI.NavMeshAgent m_NavAgentComponent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_NavAgentComponent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
        
    

    // Update is called once per frame
    private void Update()
    {
        m_NavAgentComponent.destination = Goal.position;

    }
    
}
