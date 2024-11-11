using System.Collections.Generic;
using UnityEngine;

public class AiDirector : MonoBehaviour
{
    // Singleton del AiDirector
    public static AiDirector instance;

    // Puntos de inter�s
    public Transform[] Isalarmining;      // Puntos de alarma (seg�n tu variable)
    public Transform[] waypoints;         // Puntos de patrulla
    public List<Transform> safePoints;    // Puntos seguros para el Thief
    public Transform target;              // Punto de agrupamiento
    [HideInInspector] public int currentWaypointIndex;

    // Evento de alarma
    public delegate void AlarmEvent(Vector3 position);
    public static event AlarmEvent OnAlarmTriggered;

    private void Awake()
    {
        // Implementaci�n del patr�n Singleton
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
      //  DetectAgents();
    }

    // M�todo para suministrar el siguiente punto de patrulla alcanzable
    public Transform GetNextPatrolPoint()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("No hay puntos de patrulla disponibles");
            return null;
        }

        // Selecciona el siguiente punto alcanzable en la lista de patrulla
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        return waypoints[currentWaypointIndex];
    }

    // M�todo para activar la alarma y notificar a todos los agentes
    public void TriggerAlarm(Vector3 alertPosition)
    {
        Debug.Log("Alarma activada en la posici�n: " + alertPosition);
        OnAlarmTriggered?.Invoke(alertPosition); // Notifica a todos los suscriptores
    }

    // M�todo para detectar agentes en la escena
    //private void DetectAgents()
    //{
    //    Worker[] workers = FindObjectsOfType<Worker>();
    //    Guard[] guards = FindObjectsOfType<Guard>();
    //    Thief[] thieves = FindObjectsOfType<Thief>();

    //    Debug.Log($"Se detectaron {workers.Length} Workers, {guards.Length} Guards y {thieves.Length} Thief.");

    //    // Puedes a�adir l�gica aqu� para inicializar o asignar puntos a los agentes seg�n sea necesario
    //}
}
