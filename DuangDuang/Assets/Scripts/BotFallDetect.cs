using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotFallDetect : MonoBehaviour
{
    //  The class is used purely for detecting if a bot is very close to the edge, and thus it should fall out 
    // of the platform

    private void OnTriggerEnter(Collider other)
    {
        //if the bot is close to the edge of the platform, turn the navMeshAgent off, if it falls out, it's ok
        // In bot.cs, it will handle the case it does not fall out, and re-enable navMeshAgent after x second.
        if(other.gameObject.GetComponent<NavMeshAgent>() != null)
        {
            if(other.gameObject.GetComponent<Bot>().getIsInMotionOfForce())
            {
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }


}
