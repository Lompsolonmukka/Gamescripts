using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyControllerAI2 : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent nav;

    public AudioClip found; //audio for when spotting the player
    private bool hasPlayed = false;

    [SerializeField]
    Transform[] waypoints;
    enum EnemyStates
    {
        Patrolling,
        Chasing
    }
    [SerializeField]
    EnemyStates currentState;
    int currentWaypoint = 0;
    [SerializeField]
    float decisionDelay = 3f;

    private void Start()
    {
        InvokeRepeating("SetDestination", 0.5f, decisionDelay);
        if (currentState == EnemyStates.Patrolling) nav.SetDestination(waypoints[currentWaypoint].position);
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 15f)
        {
            hasPlayed = false;
            currentState = EnemyStates.Patrolling;
        }
        else
        {
            if (!hasPlayed)
            {
                AudioSource.PlayClipAtPoint(found, transform.position);
                hasPlayed = true;
            }
            currentState = EnemyStates.Chasing;
        }




        if (currentState == EnemyStates.Patrolling)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 5f)
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            nav.SetDestination(waypoints[currentWaypoint].position);
        }
    }
    void SetDestination()
    {
        if (currentState == EnemyStates.Chasing) nav.SetDestination(player.position);
    }

    //if enemy catches player, restart scene
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }





}
