using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




public class stereo_utils : EditorWindow
{

    public Object[] quickselects=new Object[0];
    public Object[] teleporters = new Object[0];
    GameObject joueur;
    public bool showPosition = true;
    public bool showPosition2 = true;
    public bool showPosition3 = true;
    string[] bg_colors = { "5386B1", "E6BA12", "E63B12" };
    [MenuItem("stereographik/STEREO UTILS")]
    static void Init()
    {
        
        
        //Color myStyleColor = Color.red;
        // teleporters = new SerializedObject(this);
        stereo_utils window = (stereo_utils)GetWindow(typeof(stereo_utils));
        window.Show();
    }
    void OnGUI() {

        joueur = GameObject.FindGameObjectWithTag("Player");
        
        EditorGUILayout.BeginVertical("Button");        
        
        showPosition = EditorGUILayout.Foldout(showPosition, "QUICK SELECT");
        if (showPosition) {
            //GUI.backgroundColor = HexToColor(bg_colors[0]);
            // GUILayout.Space(20f);
            //GUILayout.Label("QUICK SELECT", EditorStyles.toolbarButton);
            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty stringsProperty = so.FindProperty("quickselects");
            EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
            so.ApplyModifiedProperties();
            //
            GUI.backgroundColor = HexToColor(bg_colors[0]);
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Player"))
            {
                GameObject cible = GameObject.FindGameObjectWithTag("Player");
                make_selection_active(cible as Object);
                frame_selection();
            }
            if (GUILayout.Button("Camera"))
            {
                GameObject cible = GameObject.FindGameObjectWithTag("MainCamera");
                make_selection_active(cible as Object);
                frame_selection();
                frame_selection();
            }
            EditorGUILayout.EndHorizontal();

            for (int u = 0; u < quickselects.Length; u++)
            {
                if (GUILayout.Button(quickselects[u].name))
                {
                    make_selection_active(quickselects[u] as Object);
                    frame_selection();
                }
            }
          }
        EditorGUILayout.EndVertical();
        ///
        ///
        ///
        //GUILayout.Space(20f);
        GUI.backgroundColor = HexToColor(bg_colors[1]);
        EditorGUILayout.BeginVertical("Button");
        showPosition2 = EditorGUILayout.Foldout(showPosition2, "PLAYER CONTROLER");
        if (showPosition2)
        {           
            //GUILayout.Label("PLAYER CONTROLER", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("DISABLE"))
            {

            }
            if (GUILayout.Button("ENABLE"))
            {

            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        GUI.backgroundColor = Color.white;
        ///
        ///     TELEPORT
        ///
        EditorGUILayout.BeginVertical("Button");
        showPosition3 = EditorGUILayout.Foldout(showPosition3, "TELEPORT");
        if (showPosition3)
        {
            //GUILayout.Label("TELEPORT", EditorStyles.boldLabel);
            
            ScriptableObject target2 = this;
            SerializedObject so2 = new SerializedObject(target2);
            SerializedProperty stringsProperty2 = so2.FindProperty("teleporters");
            EditorGUILayout.PropertyField(stringsProperty2, true); // True means show children
            so2.ApplyModifiedProperties();
            GUI.backgroundColor = HexToColor(bg_colors[2]);
            EditorGUILayout.BeginHorizontal("box");

            for (int u = 0; u < teleporters.Length; u++)
            {
                if (GUILayout.Button("TP " + u))
                {
                    joueur.transform.position = (teleporters[u] as GameObject).transform.position;
                }
            }
            EditorGUILayout.EndHorizontal();
            //teleporters = EditorGUILayout.ObjectField(teleporters, typeof(Object), true);
        }
        EditorGUILayout.EndVertical();
    }
    void make_selection_active(Object obj) {
        Object[] sel = new Object[1];
        sel[0] = obj;
        Selection.objects = sel;
    }
    void frame_selection() {
        SceneView.FrameLastActiveSceneView();

    }
    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
