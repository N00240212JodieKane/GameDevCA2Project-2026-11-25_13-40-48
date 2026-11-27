using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform PatrolRoute;
    public List<Transform> Locations = new List<Transform>();
    public Transform Player;

    [Header("Health Settings")]
    public int MaxHealth = 3;
    private int _currentHealth;

    private int _locationIndex = 0;
    private NavMeshAgent _agent;
    private bool _isChasing = false;

    void Start()
    {
        _currentHealth = MaxHealth;

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
        if (Locations.Count == 0) return;

        _agent.destination = Locations[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " took damage! Remaining health: " + _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " defeated!");
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enemy was hit by: " + collision.gameObject.name);

        if (collision.gameObject.name.Contains("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
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
