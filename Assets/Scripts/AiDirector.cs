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
    public Transform target;              // Punto de agrupamiento
    public Transform Interruptor;
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

    // Método para activar la alarma y notificar a todos los agentes
    public void ActivateAlarm(Vector3 alertPosition)
    {
        Debug.Log("Alarma activada en la posición: " + alertPosition);
        TriggerAlarm(alertPosition); // Llama a TriggerAlarm con la posición de alerta
    }

    // Método privado para invocar el evento de alarma
    private void TriggerAlarm(Vector3 alertPosition)
    {
        OnAlarmTriggered?.Invoke(alertPosition); // Notifica a todos los suscriptores
    }


}
