using UnityEngine;
using UnityEngine.AI;

public class Thiefmovement : MonoBehaviour
{
    public NavMeshAgent Agent;
    public float InitialVelocity = 10f; 
    private int MaintenanceMask;

    void Start()
    {
        // Definir la máscara de área en el inicio
        MaintenanceMask = 1 << NavMesh.GetAreaFromName("Maintenance");
        Debug.Log("MaintenanceMask: " + MaintenanceMask);
    }

    private void Update()
    {
        NavMeshHit hit;

        // Revisar si la posición está dentro de la zona de Maintenance en un radio de 2.0f
        if (NavMesh.SamplePosition(transform.position, out hit, 2.0f, MaintenanceMask))
        {
            Debug.Log("Agente en el área Maintenance");
            Agent.speed = InitialVelocity / 2;  // Reduce la velocidad
        }
        else
        {
            Debug.Log("Agente fuera del área Maintenance");
            Agent.speed = InitialVelocity;  // Velocidad normal
        }
    }
}