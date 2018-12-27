using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

// automaticaly map textures in material slots

// step 1 : DRAG & DROP all textures in the Maps slot
// step 2 : SELECT all the materials
// step 3 : PRESS AUTOMAP

public class automapper : EditorWindow
{
    public Texture2D[] maps;
    private Object[] library_mats;

    [MenuItem("stereographik/automapper")]    
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(automapper));

    }
    void SceneGUI(SceneView sceneView)
    {

    }
    void OnGUI()
    {
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("maps");
        EditorGUILayout.PropertyField(stringsProperty, true); 
        so.ApplyModifiedProperties();


        if (GUILayout.Button("AUTOMAP"))
        {
            map();
        }
    }
    private void OnSelectionChange()
    {

        if (Selection.activeObject is Material)
        {
            //library_mats = Selection.activeObject as Material;
            library_mats = Selection.objects;
            Debug.Log(library_mats.Length);

        }
    }
    void map()
    {
        foreach (Object obj in library_mats)
        {
            Material m = obj as Material;

            Texture2D alb = find_map(m.name, "_AlbedoTransparency");
            Texture2D metalrough = find_map(m.name, "_MetallicSmoothness");
            Texture2D norm = find_map(m.name, "_Normal");
            Texture2D emi = find_map(m.name, "_Emission");

            m.SetTexture("_MainTex", alb);
            m.SetTexture("_MetallicGlossMap", metalrough);
            m.SetTexture("_BumpMap", norm);

            if (emi != null)
            {
                m.SetTexture("_EmissionMap", emi);
            }

        }
    }

    Texture2D find_map(string matname,string maptype)
    {
        Texture2D resultat = null;
        foreach (Texture2D t in maps) {
            if (t.name.Contains(matname)) {
                if (t.name.Contains(maptype))
                {
                    resultat = t;
                }
            }


        }
        return resultat;
    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
    }
}
