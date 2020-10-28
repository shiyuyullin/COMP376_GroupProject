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

    private NavMeshAgent navMeshAgent;
    private float timer;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.stateMachine.changeState(new SearchForTarget(this.TargetItemLayer, this.gameObject, this.viewrange, this.tagToLookFor, this.targetFound));
    }

    private void FixedUpdate()
    {
        this.stateMachine.execuateStateUpdate();
    }

    public void targetFound(SearchResults searchResults)
    {

        //unpack information, furthur processing the information

        navMeshAgent.SetDestination(searchResults.allHitObjectsWithRequiredTag[0].transform.position);
        //navMeshAgent.SetDestination(new Vector3(0.0f,0.0f,0.0f));
        Debug.Log(searchResults.allHitObjectsWithRequiredTag[0].transform.position);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Obstacles") { }

        if (collision.gameObject.tag == "Player")
        {
            Vector3 forceDirection = collision.gameObject.transform.position - gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddForce(-forceDirection * recoil, ForceMode.Impulse);
        }
    }



}
