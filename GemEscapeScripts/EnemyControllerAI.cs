using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyControllerAI : MonoBehaviour
{
    private Transform player; //player position
    private NavMeshAgent nav; //AI

    public AudioClip found; //audio for when spotting the player
    private bool hasPlayed = false; //if audio has been played

    [SerializeField] Transform[] waypoints; //array for waypoints

    public enum EnemyStates //enemy is patrolling, and chase only when player is seen
    {
        Patrolling,
        Chasing
    }

    [SerializeField] EnemyStates currentState;
    int currentWaypoint = 0;
    [SerializeField] float decisionDelay = 3f; //time before next waypoint is targeted


    public float fadeDuration = 1f; //screen fade
    public float displayDuration = 2f; //how long the picture will stay before restart
    public GameObject Oplayer;
    public PlayerController winner;

    public CanvasGroup GameOverCanvasGroup; //canvas for the images if game over
    public bool isPlayerCaught = false; //is player caught by the enemy
    private float timer;
    





    private void Start()
    {
        InvokeRepeating("SetDestination", 0.5f, decisionDelay);
        if (currentState == EnemyStates.Patrolling)
            nav.SetDestination(waypoints[currentWaypoint].position); //if patrolling, follow waypoints

    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    public void Update()
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
        if (isPlayerCaught)
        {
            Oplayer.GetComponent<PlayerController>().enabled = false;
            EndLevel();
        }
    }

    void SetDestination()
    {
        if (currentState == EnemyStates.Chasing) nav.SetDestination(player.position);
    }

    //if enemy catches player, restart scene
    private void OnCollisionEnter(Collision collision) //if enemy collides with player, change Ending boolean to true
    {
        if (collision.transform.name == "Player")
        {
            isPlayerCaught = true;
        }
    }

    public void EndLevel()
    {
        timer += Time.deltaTime; //timer equal to itself plus deltatime
        GameOverCanvasGroup.alpha = timer / fadeDuration; //set the alpha on canvas group

        if (timer > fadeDuration + displayDuration)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart scene
        }
    }
}
