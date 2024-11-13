using System.Collections.Generic;
using UnityEngine;

public class AiDirector : MonoBehaviour
{
    public static AiDirector instance;

    // Puntos de interés
    public Transform[] Isalarmining;      // Puntos de alarma
    public Transform[] Guardwaypoints;         // Puntos de patrulla
    public Transform[] Workerwaypoints;         // Puntos de patrulla
    public Transform[] Thiefwaypoints;         // Puntos de patrulla
    public List<Transform> safePoints;    // Puntos seguros para el Thief
    public Transform  Puntodeagrupamiento;              // Punto de agrupamiento
    public Transform [] Interruptor; // interruptor
    [HideInInspector] public int currentWaypointIndex;

    // Evento de alarma
    public delegate void AlarmEvent(Vector3 position);
    public static event AlarmEvent OnAlarmTriggered;

    private void Awake()
    {
        // Implementación del patrón Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Detecta los agentes en la escena al inicio
        // DetectAgents();  // Puedes activar esto si quieres detectar los agentes en Start
    }

    // Método para suministrar el siguiente punto de patrulla alcanzable
    public Transform GetNextPatrolPoint()
    {
        if (Workerwaypoints == null || Workerwaypoints.Length == 0)
        {
            Debug.LogError("No hay puntos de patrulla disponibles");
            return null;
        }

        // Selecciona el siguiente punto alcanzable en la lista de patrulla
        currentWaypointIndex = (currentWaypointIndex + 1) % Workerwaypoints.Length;
        return Workerwaypoints[currentWaypointIndex];
    }

    // Método para encontrar el interruptor más cercano
    public Transform GetClosestSwitchPoint(Vector3 workerPosition)
    {
        Transform closestSwitch = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform switchPoint in Interruptor)
        {
            float distance = Vector3.Distance(workerPosition, switchPoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestSwitch = switchPoint;
            }
        }
        return closestSwitch;
    }

    // Método para activar la alarma
    public void TriggerAlarm(Vector3 alertPosition)
    {
        Debug.Log("Alarma activada en la posición: " + alertPosition);
        // Aquí se pueden implementar acciones adicionales para el estado de alarma
    }
    public Transform GetPuntodeagrupamiento()
    {
        return Puntodeagrupamiento;
    }


}
