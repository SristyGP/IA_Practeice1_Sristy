using UnityEngine;

public class Agent : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    public Vector3 target;
    

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        UpdateDestination();

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            GoToNextWaypoint(); // Cambiar al siguiente punto
        }
  

    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position; // Asigna el siguiente punto como destino
        agent.SetDestination(target); // Le dices al agente que vaya hacia ese destino
    }

    void GoToNextWaypoint()
    {
        waypointIndex = (waypointIndex + 1) % waypoints.Length; // Moverse al siguiente punto en la lista, en bucle
        UpdateDestination(); // Actualizar el destino
    }
}
