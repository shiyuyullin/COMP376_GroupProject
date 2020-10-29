using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class SearchForTarget : IState
{
    private LayerMask searchLayer;
    private GameObject ownerGameObject;
    private float searchRadius;
    private string tagToLookFor;
    //A method that takes SearchResults
    public System.Action<SearchResults> searchResultsCallBack; 

    public SearchForTarget(LayerMask searchLayer, GameObject owner, float searchRadius,string tagToLookFor, Action<SearchResults> searchResultsCallBack)
    {
        this.searchLayer = searchLayer;
        this.ownerGameObject = owner;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.searchResultsCallBack = searchResultsCallBack;
    }

    public void Enter()
    {
        
    }

    public void Execuate()
    {
        var hitObjects = Physics.OverlapSphere(this.ownerGameObject.transform.position, searchRadius, searchLayer);
        var allObjectsWithRequiredTags = new List<Collider>();
        for (int i = 0; i < hitObjects.Length; i++)
        {
           if (hitObjects[i].CompareTag(tagToLookFor)&&hitObjects[i].gameObject!=ownerGameObject&&hitObjects[i].gameObject!=null)
           {
               
               allObjectsWithRequiredTags.Add(hitObjects[i]);
           }
        }
           
        
        
        this.searchResultsCallBack(new SearchResults(hitObjects, allObjectsWithRequiredTags));
            
        
        
    }
    public void Exit()
    {
        
    }
}

public class SearchResults
{
    public Collider[] allHitObjectInSearchRadius;

    public List<Collider> allHitObjectsWithRequiredTag;

    public SearchResults(Collider[] allHitObjectInSearchRadius, List<Collider> allHitObjectsWithRequiredTag)
    {
        this.allHitObjectInSearchRadius = allHitObjectInSearchRadius;
        this.allHitObjectsWithRequiredTag = allHitObjectsWithRequiredTag;

        //methods to provide more information
    }
}
