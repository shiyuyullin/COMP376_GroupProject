using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teaCups : MonoBehaviour
{
    public GameObject teaCup;
    public GameObject fallDetective;
    float teaCupTimer;
    GameObject [] teaCups_ = new GameObject[5];
    float angle;
    float r;
    float distance=12;
    // Start is called before the first frame update
    public Material [] ms = new Material[5];
    void Start()
    {
        r=distance;
        angle=0;
        float temp;
        float x;
        float z;
        float y;
        for(int i =0 ; i<5; i++){
            temp=i/5.0f *2*Mathf.PI;
            x=distance*Mathf.Cos(temp);
            z=distance*Mathf.Sin(temp);
            y=0;

            teaCups_[i]= Instantiate(teaCup,new Vector3(x+2,y,z-2),Quaternion.identity);
            
            //Material m = (Material)Resources.Load(string.Format("/Materials/Map-L/{0}",i), typeof(Material));
            
            teaCups_[i].GetComponent<MeshRenderer> ().material = ms[i];
        }
        for (int i =0; i<20;i++){
            temp=i/20.0f *2*Mathf.PI+Mathf.PI/20.0f;
            x=25*Mathf.Cos(temp);
            z=25*Mathf.Sin(temp);
            y=0;
            Instantiate(fallDetective,new Vector3(x,y,z),Quaternion.Euler(0,((Mathf.PI/2-temp)/Mathf.PI)*180,0));
        }
        teaCupTimer=0;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        teaCupTimer+= Time.deltaTime;
        moveTeaCup(teaCupTimer);
    }
    void moveTeaCup(float timer){
        float temp=angle;
        angle=angle+Time.deltaTime/((300-timer)*0.01f);
        if(angle > 2* Mathf.PI){
            angle-=2*Mathf.PI;
        }
        r=distance*(Mathf.Cos(timer/((300-timer)*0.01f))+1)/2.0f+8;
        for(int i =0 ; i<5; i++){
            
            if(temp > 2* Mathf.PI){
                temp-=2*Mathf.PI;
            }
            
            float x=r*Mathf.Cos(temp);
            float z=r*Mathf.Sin(temp);
            float y=0;

            teaCups_[i].transform.position=new Vector3(x,y,z);
            temp+=1/5.0f *2*Mathf.PI;;
            }

    }
}
