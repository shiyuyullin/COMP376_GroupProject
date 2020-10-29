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
    [SerializeField] public List<GameObject> chaseYouGameObject;
    [SerializeField] private float timer;
    [SerializeField] private float collisonTime;
   
    private void Start()

    {

        chaseYouGameObject = new List<GameObject>();
        timer=0;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        collisonTime=-1;
        chaseTarget=null;
        //the default state is to chase item
        this.stateMachine.changeState(new SearchForTarget(this.TargetItemLayer, this.gameObject, this.viewrange, this.tagToLookFor, this.targetFound));
        state="chase";
        
        
    }

    private void Update()
    {
        //if the bot is off the edge
        if(transform.position.y<-10||transform.position.x<0||transform.position.x>50||transform.position.z<0||transform.position.z>50){
            gameObject.tag="Untagged";
            GameObject [] temp = GameObject.FindGameObjectsWithTag("Target");
            for(int i=0; i<temp.Length;i++){
                if(temp[i].name=="Bumper Car"){
                    continue;
                }
                if(temp[i]==gameObject){
                    continue;
                }
                temp[i].GetComponent<Bot>().chaseYouGameObject.Remove(gameObject);
                if(temp[i].GetComponent<Bot>().chaseTarget==gameObject){
                    temp[i].GetComponent<Bot>().chaseTarget=null;
                }

            }
            temp=null;
            return;
        }
        //re enable navMeshAgent after 2 sec collision
        if(navMeshAgent.enabled==false){
            collisonTime+=Time.deltaTime;
            if(collisonTime>=2){
            navMeshAgent.enabled=true;
            collisonTime=-1;
            }
            else{
                return;
            }
        }
        
       //execuate state
        this.stateMachine.execuateStateUpdate();
        
    }

    public void targetFound(SearchResults searchResults)

    {   
        //if there is two and more bot chase you start to escape, so the bots don't all stack together
        if(chaseYouGameObject.Count>=2){
            this.stateMachine.changeState(new Escape( this.gameObject,this.calledFromEscape));
            chaseTarget=null;
            state="escape";
            
            return;
        }
        
        timer += Time.deltaTime;
        if(chaseTarget==null){
            int chaseIndex =Random.Range(0,searchResults.allHitObjectsWithRequiredTag.Count);
            chaseTarget=searchResults.allHitObjectsWithRequiredTag[chaseIndex].gameObject;
            if(chaseTarget.name!="Bumper Car"&&chaseTarget!=null){
                // notice chase target that i am chase you
            chaseTarget.GetComponent<Bot>().chaseYouGameObject.Add(this.gameObject);
            
            }
        }
        // change chase target every 5 sec
        else if(timer>=5){
            timer =0;
            if(chaseTarget.name!="Bumper Car"&&chaseTarget!=null){
            // notice chase target that i am not chasing anymore you
            chaseTarget.GetComponent<Bot>().chaseYouGameObject.Remove(this.gameObject);}
            int chaseIndex =Random.Range(0,searchResults.allHitObjectsWithRequiredTag.Count);
            chaseTarget=searchResults.allHitObjectsWithRequiredTag[chaseIndex].gameObject;
            if(chaseTarget.name!="Bumper Car"&&chaseTarget!=null){
            // notice chase target that i am chase you
            chaseTarget.GetComponent<Bot>().chaseYouGameObject.Add(this.gameObject);}
            
        }
        if(chaseTarget!=null){
            navMeshAgent.SetDestination(chaseTarget.transform.position);
        }
       
        

    }

    public void calledFromEscape(Vector3 direction){
        //if there is not bot chase you change state from escape to chase
        if(direction.y==-1){
            stateMachine.changeState(new SearchForTarget(this.TargetItemLayer, this.gameObject, this.viewrange, this.tagToLookFor, this.targetFound));
            chaseTarget=null;
            
            return;
        }
        // if the escape destination is off the map change direction
        if(transform.position.z<=10){
            navMeshAgent.SetDestination(new Vector3(direction.x,direction.y,direction.z+20));
            
            return;
        }
        if(transform.position.z>=40){
            navMeshAgent.SetDestination(new Vector3(direction.x,direction.y,direction.z-20));
            
            return;
        }
        if(transform.position.x<=10){
            navMeshAgent.SetDestination(new Vector3(direction.x+20,direction.y,direction.z+20));
           
            return;
        }
        if(transform.position.x>=40){
            navMeshAgent.SetDestination(new Vector3(-direction.x-20,direction.y,direction.z-20));
            
            return;
        }

        navMeshAgent.SetDestination(direction);
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Obstacles") { }

        if(collision.collider.CompareTag("Target"))
        {
            GetComponent<NavMeshAgent>().enabled = false;
            collisonTime=0;
            Vector3 forceDirection = collision.gameObject.transform.position - gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddForce(-forceDirection * recoil, ForceMode.Impulse);
        }
    }



}
