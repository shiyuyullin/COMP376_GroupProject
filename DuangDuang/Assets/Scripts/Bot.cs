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
    private NavMeshAgent navMeshAgent;
    private float timer;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.stateMachine.changeState(new SearchForTarget(this.TargetItemLayer, this.gameObject, this.viewrange, this.tagToLookFor, this.targetFound));
    }

    private void Update()
    {
        this.stateMachine.execuateStateUpdate();
    }

    public void targetFound(SearchResults searchResults)
    {
        timer += Time.deltaTime;
        //unpack information, furthur processing the information
        if(timer <= 3.0f)
        {
            navMeshAgent.SetDestination(searchResults.allHitObjectsWithRequiredTag[0].transform.position);
        }
        
        
    }


}
