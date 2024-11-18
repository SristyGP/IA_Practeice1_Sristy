using System.Collections.Generic;
using UnityEngine;

public class AiDirector : MonoBehaviour
{
    public static AiDirector instance;

    // Puntos de interés
    public Transform[] Isalarmining;      // Puntos de alarma
    private float AlarmDuration = 15.0f; // duración de la alarma activada
    private float alarmStartTime;
    public bool isAlarmActive = false; // Estado de la alarma

    public Transform[] Guardwaypoints;         // Puntos de patrulla
    public Transform[] Workerwaypoints;         // Puntos de patrulla
    public Transform[] Thiefwaypoints;         // Puntos de patrulla
    public Transform[] SafePoints;    // Puntos seguros para el Thief
    public Transform  Puntodeagrupamiento;              // Punto de agrupamiento
    public Transform[] Interruptor; // interruptor
    [HideInInspector] public int currentWaypointIndex;
    public Vector3 thiefReportedPosition;



    [SerializeField] private GameObject[] Workers;
    [SerializeField] private GameObject[] Thiefs;
    [SerializeField] private GameObject[] Guards;
    

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
        //Detecta a los agentes en la escena
        Guards = GameObject.FindGameObjectsWithTag("Guard");
        Thiefs = GameObject.FindGameObjectsWithTag("Thief");
        Workers = GameObject.FindGameObjectsWithTag("Worker");
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
    public Transform GetClosestSafePoint(Vector3 currentPosition)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform safePoint in SafePoints)
        {
            float distance = Vector3.Distance(currentPosition, safePoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = safePoint;
            }
        }

        return closest;
    }

    // Método para activar la alarma
    public void TriggerAlarm(Vector3 alertPosition)
    {
        Debug.Log("Alarma activada en la posición: " + alertPosition);
        isAlarmActive = true; // Activa la alarma
        alarmStartTime = Time.time; // Registra el momento de inicio de la alarma

        //Afecta a todos los workers de la escena
        foreach (GameObject workers in Workers) 
        {
            Animator animator = workers.GetComponent<Animator>();
            //animator.SetBool("To Alarm", true); - boleano
            animator.SetTrigger("ToAlarm"); 
        }
        foreach (GameObject thiefs in Thiefs)
        {
            Animator animator = thiefs.GetComponent<Animator>(); // accedo a la variable creada thiefs
            animator.SetTrigger("ToAlarmThief");
        }
        foreach (GameObject guards in Guards)
        {
            Debug.Log("Guardia detectado ");

          Animator animator = guards.GetComponent<Animator>(); // accedo a la variable creada guards
            
            animator.SetTrigger("ToAlarmGuard");
        }

       
    }

    private void Update()
    {
         if (isAlarmActive && Time.time - alarmStartTime >= AlarmDuration)
        {
            Debug.Log("La alarma se ha desactivado tras 15 segundos.");
            isAlarmActive = false; // Desactiva el estado de la alarma

            foreach (GameObject thiefs in Thiefs)
            {
                Animator animator = thiefs.GetComponent<Animator>(); // accedo a la variable creada thiefs
                animator.SetTrigger("ToSearch");
            }
        }
    }
    public Transform GetPuntodeagrupamiento()
    {
        return Puntodeagrupamiento;
    }

    public void ReportThiefPosition(Vector3 position)
    {
        Debug.Log("Thief detectado en la posición: " + position);
        thiefReportedPosition = position;
    }

}
