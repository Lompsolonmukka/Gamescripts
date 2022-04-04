using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    //DOES NOT WORK YET..ENEMY SEES THROUGH WALLS?

    public Transform player; //target transform for position check
    private bool isPlayerInRange; //bool variable for ontriggerenter method
    public EnemyControllerAI.EnemyStates currentState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player) //is player on trigger collider
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) //player leaving trigger
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange) //check if the player is in sight
        {
            Vector3 direction = player.position - transform.position;  //pointofview objects direction is from playerposition minus pointofview
            Ray ray = new Ray(transform.position, direction); //create ray from pointofview
            RaycastHit raycastHit; //information for the objects found by raycast

            if (Physics.Raycast(ray, out raycastHit)) //if ray hits something, out data
            {
                if (raycastHit.collider.transform == player) //check if the player has been seen by the pointofview collider
                {
                    currentState = EnemyControllerAI.EnemyStates.Chasing;
                }
            }
        }
    }
}
