///// RAYCASTER

        RaycastHit hit;
        Vector3 lookdir = camtransform.forward;
        // 
        if (Physics.Raycast(camtransform.position, lookdir, out hit))
        {
        if (hit.collider.gameObject.tag == "Player" && hit.distance<100f) {               
            // HIT                              
            }
        }
        
        
///// COLLIDERS        
        void OnTriggerEnter(Collider other) {
          if(other.gameobject.tag =="Player"){
            // HIT IN
          }
        }
        
        void OnTriggerExit(Collider other) {
          if(other.gameobject.tag =="Player"){
            // HIT OUT
          }
        }
