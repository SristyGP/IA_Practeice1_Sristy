using UnityEngine;

public class AiDirector : MonoBehaviour
{
    public static AiDirector instance;
    public Transform[]  Isalarmining;
    public Transform[] waypoints;
    public Transform target;
    [HideInInspector] public int currentWaypointIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = new AiDirector(); // si no se detecta el game manager, crea uno nuevo. Si no se crea uno nuevo, este se destruye.
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
