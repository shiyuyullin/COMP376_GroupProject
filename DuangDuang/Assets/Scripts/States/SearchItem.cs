using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class SearchItem: IState
{
   
    Action<Vector3> searchItem;
    GameObject ownGO;
    LayerMask itemLayer;
    public SearchItem(GameObject ownGO_,Action<Vector3> searchItem_){
        itemLayer = LayerMask.GetMask("Item");
        ownGO=ownGO_;
        searchItem=searchItem_;
    }
    // Start is called before the first frame update
     public void Enter()
    {
        var hitObjects = Physics.OverlapSphere(ownGO.transform.position, 10, itemLayer);
        if(hitObjects.Length>0){
           searchItem(hitObjects[0].gameObject.transform.position);
            
            
        }
        else
        {
            searchItem(new Vector3(-1,-1,-1));
        }
    }
   public void Execuate()
    {
      var hitObjects = Physics.OverlapSphere(ownGO.transform.position, 10, itemLayer);
        if(hitObjects.Length>0){
           searchItem(hitObjects[0].gameObject.transform.position);
            
            
        }
        else
        {
            searchItem(new Vector3(-1,-1,-1));
        }
    }
    // Update is called once per frame
    public void Exit(){

    }
}
