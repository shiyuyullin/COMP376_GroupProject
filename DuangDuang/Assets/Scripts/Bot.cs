using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    private StateMachine stateMachine = new StateMachine();
    [SerializeField] private LayerMask TargetItemLayer;
    [SerializeField] private float viewrange;
    [SerializeField] private string tagToLookFor;
    [SerializeField] float forceMagnitude;
    [SerializeField] float recoil;
    [SerializeField] public GameObject chaseTarget;
    //just to see in the unity
    [SerializeField] private string state;
    private NavMeshAgent navMeshAgent;
    //The bots that is currently chasing me
    [SerializeField] public List<GameObject> chaseYouGameObject;
    [SerializeField] private float timer;
    [SerializeField] private float collisonTime;
    // Detecting if the object is hitted and is in motion caused by force, if yes, it should not allow to move, if no, it can move
    private bool InMotionOfForce;
    //This is just use for keeping a time, such that after x second the navMeshAgent is re-enabled.
    private float edgeDetectTimer;
    private bool isOnThePlane;

    private void Start()
    {

        chaseYouGameObject = new List<GameObject>();
        timer = 0;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        collisonTime = -1;
        chaseTarget = null;
        //the default state is to chase item
        this.stateMachine.changeState(new SearchForTarget(this.TargetItemLayer, this.gameObject, this.viewrange, this.tagToLookFor, this.targetFound));
        state = "chase";
    }

    private void Update()
    {
        //if the bot is off the edge
        //if (transform.position.y < -10 || transform.position.x < 0 || transform.position.x > 50 || transform.position.z < 0 || transform.position.z > 50)
        //{
        //    gameObject.tag = "Untagged";
        //    GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        //    for (int i = 0; i < temp.Length; i++)
        //    {
        //        if (temp[i].name == "Bumper Car")
        //        {
        //            continue;
        //        }
        //        if (temp[i] == gameObject)
        //        {
        //            continue;
        //        }
        //        temp[i].GetComponent<Bot>().chaseYouGameObject.Remove(gameObject);
        //        if (temp[i].GetComponent<Bot>().chaseTarget == gameObject)
        //        {
        //            temp[i].GetComponent<Bot>().chaseTarget = null;
        //        }

        //    }
        //    temp = null;
        //    return;
        //}
        //re enable navMeshAgent after 2 sec collision
        //if (navMeshAgent.enabled == false)
        //{
        //    collisonTime += Time.deltaTime;
        //    if (collisonTime >= 2)
        //    {
        //        navMeshAgent.enabled = true;
        //        collisonTime = -1;
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        
        //Checking if I am within the platform
        
        // if the navMeshAgent is disabled, which can only be disabled by BotFallDetect.cs
        if(this.navMeshAgent.enabled == false)
        {
            edgeDetectTimer += Time.deltaTime;
            if(edgeDetectTimer >= 3.0 && isOnThePlane)
            {
                this.navMeshAgent.enabled = true;
                edgeDetectTimer = 0.0f;
            }
        }

        if (InMotionOfForce)
        {
            if (this.navMeshAgent.velocity.magnitude <= 0.2)
            {
                InMotionOfForce = false;
            }
        }

        if (chaseTarget != null && this.navMeshAgent.enabled != false) 
        {
            navMeshAgent.SetDestination(chaseTarget.transform.position);
        }

        //execuate state
        this.stateMachine.execuateStateUpdate();

    }

    public void targetFound(SearchResults searchResults)
    {
        //if there is two and more bot chase you start to escape, so the bots don't all stack together
        if (chaseYouGameObject.Count >= 2)
        {
            this.stateMachine.changeState(new Escape(this.gameObject, this.calledFromEscape));
            chaseTarget = null;
            state = "escape";

            return;
        }

        timer += Time.deltaTime;
        if (chaseTarget == null)
        {
            int chaseIndex = Random.Range(0, searchResults.allHitObjectsWithRequiredTag.Count);
            chaseTarget = searchResults.allHitObjectsWithRequiredTag[chaseIndex].gameObject;
            if (chaseTarget.name != "Bumper Car" && chaseTarget != null)
            {
                // notice chase target that i am chase you
                chaseTarget.GetComponent<Bot>().chaseYouGameObject.Add(this.gameObject);

            }
        }
        // change chase target every 5 sec
        else if (timer >= 5)
        {
            timer = 0;
            if (chaseTarget.name != "Bumper Car" && chaseTarget != null)
            {
                // notice chase target that i am not chasing anymore you
                chaseTarget.GetComponent<Bot>().chaseYouGameObject.Remove(this.gameObject);
            }
            int chaseIndex = Random.Range(0, searchResults.allHitObjectsWithRequiredTag.Count);
            chaseTarget = searchResults.allHitObjectsWithRequiredTag[chaseIndex].gameObject;
            if (chaseTarget.name != "Bumper Car" && chaseTarget != null)
            {
                // notice chase target that i am chase you
                chaseTarget.GetComponent<Bot>().chaseYouGameObject.Add(this.gameObject);
            }
        }

    }

    public void calledFromEscape(Vector3 direction)
    {
        //if there is not bot chase you change state from escape to chase
        if (direction.y == -1 && navMeshAgent.enabled != false)
        {
            stateMachine.changeState(new SearchForTarget(this.TargetItemLayer, this.gameObject, this.viewrange, this.tagToLookFor, this.targetFound));
            chaseTarget = null;

            return;
        }
        // if the escape destination is off the map change direction
        if (transform.position.z <= 10 && navMeshAgent.enabled != false)
        {
            navMeshAgent.SetDestination(new Vector3(direction.x, direction.y, direction.z + 20));

            return;
        }
        if (transform.position.z >= 40 && navMeshAgent.enabled != false)
        {
            navMeshAgent.SetDestination(new Vector3(direction.x, direction.y, direction.z - 20));

            return;
        }
        if (transform.position.x <= 10 && navMeshAgent.enabled != false)
        {
            navMeshAgent.SetDestination(new Vector3(direction.x + 20, direction.y, direction.z + 20));

            return;
        }
        if (transform.position.x >= 40 && navMeshAgent.enabled != false)
        {
            navMeshAgent.SetDestination(new Vector3(-direction.x - 20, direction.y, direction.z - 20));

            return;
        }
        if(navMeshAgent.enabled != false)
        {
            navMeshAgent.SetDestination(direction);
        }
        

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Obstacles") { }

        if (collision.collider.CompareTag("TeamA") || collision.collider.CompareTag("TeamB"))
        {
            //GetComponent<NavMeshAgent>().enabled = false;
            //collisonTime = 0;
            Vector3 forceDirection = collision.gameObject.transform.position - gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddForce(-forceDirection * recoil, ForceMode.Impulse);
            this.InMotionOfForce = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isOnThePlane = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isOnThePlane = false;
        }
    }
    public void setIsInMotionOfForce(bool temp)
    {
        this.InMotionOfForce = temp;
    }

    public bool getIsInMotionOfForce()
    {
        return InMotionOfForce;
    }

}