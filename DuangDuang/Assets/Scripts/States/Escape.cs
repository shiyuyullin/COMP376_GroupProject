using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Escape: IState
{
     private GameObject ownerGameObject;
     private Bot ownerScript;
     public System.Action<Vector3> directionCallBack;



     public Escape (GameObject ownerGameObject_,System.Action<Vector3> directionCallBack_){
        ownerGameObject=ownerGameObject_;
        ownerScript= ownerGameObject_.GetComponent<Bot>();
        directionCallBack=directionCallBack_;
     }

     public void Enter(){
        if(ownerScript.chaseYouGameObject.Count<=0){
            directionCallBack(new Vector3(-1,-1,-1));
            return;
        }
        if(ownerScript.chaseYouGameObject.Count==1){
            directionCallBack((ownerGameObject.transform.position-ownerScript.chaseYouGameObject[0].transform.position).normalized);
        }
        if(ownerScript.chaseYouGameObject.Count>1){
            directionCallBack(ownerScript.chaseYouGameObject[0].transform.position-1/2*(ownerScript.chaseYouGameObject[0].transform.position-ownerScript.chaseYouGameObject[1].transform.position).normalized);
        }
        

     }
     public void Execuate(){
         if(ownerScript.chaseYouGameObject.Count<=0){
            directionCallBack(new Vector3(-1,-1,-1));
            return;
        }

        Vector3 temp=ownerScript.chaseYouGameObject[UnityEngine.Random.Range(0,ownerScript.chaseYouGameObject.Count)].transform.position;
        temp=(ownerGameObject.transform.position-temp).normalized;
        directionCallBack(temp);

     }
     public void  Exit(){

     }
}