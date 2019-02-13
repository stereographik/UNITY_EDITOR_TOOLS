using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partgen : MonoBehaviour
{

    SkinnedMeshRenderer smr;
    Vector3[] verts;
    GameObject[] instances;
    Mesh mesh_emiter;
    public GameObject reference;
    float[] greyvalues;

    // Start is called before the first frame update
    void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        mesh_emiter = new Mesh();
        smr.BakeMesh(mesh_emiter);
        verts = mesh_emiter.vertices;
        instances = new GameObject[verts.Length];
        greyvalues = new float[verts.Length];
        for (int i = 0; i < verts.Length; i++)
        {
           
            Texture2D snap = smr.gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;
            Vector2 uvcoord = mesh_emiter.uv[i];
            float coordx = uvcoord[0] * 1024.0f;
            float coordy = uvcoord[1] * 1024.0f;
            Color tint = snap.GetPixel((int)coordx, (int)coordy);
            greyvalues[i] = tint.b;

            //
            instances[i] = GameObject.Instantiate(reference);//GameObject.CreatePrimitive(PrimitiveType.Cube);
            instances[i].transform.localScale = Vector3.one / 10f;
            instances[i].GetComponent<TrailRenderer>().time = (1-greyvalues[i])/3f;
            //


        }



        StartCoroutine(refreshvertices());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator refreshvertices()
    {
        mesh_emiter = new Mesh();
        smr.BakeMesh(mesh_emiter);
        verts = mesh_emiter.vertices;
        for (int i = 0; i < verts.Length; i++)
        {          
            instances[i].transform.position = transform.TransformPoint(verts[i]/transform.parent.localScale.x); 
        }


        yield return new WaitForSeconds(.05f);
        StartCoroutine(refreshvertices());
    }



  

}
