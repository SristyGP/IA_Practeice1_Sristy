using UnityEngine;

public class Agent : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform target;
    [HideInInspector] public int currentWaypointIndex;

}
