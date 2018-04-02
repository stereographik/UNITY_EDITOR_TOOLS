//C# Example
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class objects_terrain_spawner : EditorWindow
{

    public int nb_groups = 0;
    int lastgroup_value=0;
    public Object[] lod_objs = new Object[0];
    public float pond;
    public Vector4 scale_range = new Vector4(.1f,.8f,0.001f,1f);
    public Vector4 random_orientation =Vector3.zero;
    public float rayon = 1;
    public float density = 1;
    public Object origine;
    public Vector3 offset_pos = Vector3.zero;
    public float main_scale =1f;
    GameObject container;




    [MenuItem("stereographik/TERRAIN OBJECT SPAWNER")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(objects_terrain_spawner));
        
    }
    void Start() {
       
    }
    void OnGUI()
    {

        


        GUILayout.Label("SPAWN CONFIG", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Transform origine");
        origine = EditorGUILayout.ObjectField(origine, typeof(GameObject), true);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);
        EditorGUILayout.BeginVertical("Button");        
            EditorGUI.indentLevel+=3;
            EditorGUILayout.BeginFadeGroup(1);
                                   
            GUILayout.Label("ASSETS REFERENCES ", EditorStyles.boldLabel);           
           // pond[u] = GUILayout.HorizontalSlider( pond[u], 0.0F, 100.0F);           
           ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);            
            SerializedProperty stringsProperty = so.FindProperty("lod_objs");
            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties(); // Remember to apply modified properties
            
            EditorGUILayout.EndFadeGroup();
        EditorGUI.indentLevel -= 3;
        EditorGUILayout.BeginHorizontal("Button");
        //EditorGUILayout.LabelField("scale range");
        EditorGUILayout.LabelField("Main scale factor");
        main_scale = EditorGUILayout.FloatField(main_scale);

       // EditorGUILayout.LabelField("Random scale range "+ scale_range.x+" - "+ scale_range.y);
        EditorGUILayout.MinMaxSlider(ref scale_range.x, ref scale_range.y, scale_range.z, scale_range.w);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Random rotation (-X +X | -Y +Y | -Z +Z)");
        random_orientation=EditorGUILayout.Vector3Field("",random_orientation);
        //EditorGUILayout.EndHorizontal();
        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);        
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical("Button");
        EditorGUILayout.BeginFadeGroup(1);
        GUILayout.Label("OPTIONS", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Rayon");
        rayon=EditorGUILayout.Slider(rayon, 0f, 100f);
        EditorGUILayout.LabelField("Density");
        density=EditorGUILayout.Slider(density, 0f, 100f);
        EditorGUILayout.LabelField("Offset position");
        offset_pos = EditorGUILayout.Vector3Field("", offset_pos);
        
        //random_rotation.x = 
        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginHorizontal("Button");
        GUILayout.Space(25f);
        if (GUILayout.Button("C R E A T E"))
        {
            spawn_assets();
        }
        if (GUILayout.Button("R E M O V E"))
        {
            clear_assets();
        }
        EditorGUILayout.EndHorizontal();
    }
    void spawn_assets() {
        if (GameObject.Find("TERRAIN OBJECT SPAWNER") == null)
        {
            container = new GameObject();

        }
        Vector3[] pos_on_ground = get_raycast_hits();
        for (int u = 0; u < lod_objs.Length; u++) {

            GameObject Aspawn = Instantiate(lod_objs[u] as GameObject);
            Aspawn.tag = "veget";
            Aspawn.transform.position = pos_on_ground[u];
            //
            float spawnscale = Random.Range(scale_range.x, scale_range.y)* main_scale;
            Aspawn.transform.localScale=Vector3.one*spawnscale;

            float rand_rot_x = Random.Range(-random_orientation.x, random_orientation.x);
                float rand_rot_y = Random.Range(-random_orientation.y, random_orientation.y);
            float rand_rot_z = Random.Range(-random_orientation.z, random_orientation.z);
            Aspawn.transform.rotation = Quaternion.Euler(rand_rot_x, rand_rot_y, rand_rot_z);
            //
            Aspawn.transform.SetParent(container.transform);
        }

    }
    void clear_assets() {
        GameObject[] allveget = GameObject.FindGameObjectsWithTag("veget");
        for (int u = 0; u < allveget.Length; u++)
        { 
           DestroyImmediate(allveget[u]);
        }

    }
    Vector3[] get_raycast_hits() {
        int nb_rays = lod_objs.Length;// Mathf.RoundToInt(density * 10f);
        Vector3[] resultat = new Vector3[nb_rays];
        Vector3 ori = (origine as GameObject).transform.position;
        for (int u = 0; u < nb_rays; u++)
        {
            Vector2 rand_around = Random.insideUnitCircle * rayon;
            Vector3 ray_origine = ori + new Vector3(rand_around.x, 0, rand_around.y) + (Vector3.up * 10.0f);
            RaycastHit hit;
            resultat[u] =Vector3.zero;
            if (Physics.Raycast(ray_origine, -Vector3.up, out hit)) {
                if (hit.collider.gameObject.name == "Terrain") {
                    resultat[u] = hit.point;
                }            
        }
        }


        return resultat;
    }
   
}