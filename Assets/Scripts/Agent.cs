using UnityEngine;

public class Agent : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
