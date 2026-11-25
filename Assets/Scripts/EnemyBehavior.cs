using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform PatrolRoute;
    public List<Transform> Locations = new List<Transform>();
    public Transform Player;

    private int _locationIndex = 0;
    private NavMeshAgent _agent;
    private bool _isChasing = false; // <-- Added missing variable

    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            Player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Could not find a GameObject named 'Player' in the scene!");
        }

        _agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        // <-- Added chase logic in Update()
        if (_isChasing && Player != null)
        {
            _agent.destination = Player.position;
        }
        else if (_agent.remainingDistance < 0.2f && !_agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void InitializePatrolRoute()
    {
        if (PatrolRoute == null)
        {
            Debug.LogError("PatrolRoute is not assigned on " + gameObject.name);
            return;
        }

        foreach (Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0)
            return;

        _agent.destination = Locations[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (other.CompareTag("Player") || other.transform.root.name == "Player")
        {
            _isChasing = true;
            Debug.Log("Enemy detected!!!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.transform.root.name == "Player")
        {
            _isChasing = false;
            Debug.Log("Player out of range, resume patrol");
            MoveToNextPatrolLocation();
        }
    }
}
