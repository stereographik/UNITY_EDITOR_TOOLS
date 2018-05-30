using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_activities : MonoBehaviour
{

    public float camera_fov;         
    public float angle_range;
    GameObject joueur;   
    
    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   bool is_in_sight(){
   bool resultat=false;
    //
     angle_range = Mathf.Cos(Mathf.Deg2Rad * camera_fov);
      Vector3 direction = Vector3.Normalize(transform.position- joueur.transform.position);
            float dot = Vector3.Dot(direction, joueur.transform.forward);
           //Debug.Log(dot);
            //
            if (dot> angle_range)
            {
            resultat=true;
            }            
            return resultat;
   }
}
