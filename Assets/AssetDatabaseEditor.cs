using UnityEditor;
using UnityEngine;

public class AssetDatabaseEditor : EditorWindow
{
    public Object AssetObject;
    public Object[] ObjectsToAdd;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Tools/AssetDatabaseEditor")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(AssetDatabaseEditor), false, "ADE");
    }

    void OnGUI()
    {
        EditorGUILayout.Space();

        // Show asset field
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Asset");
        AssetObject = EditorGUILayout.ObjectField(AssetObject, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        // Show array
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("ObjectsToAdd");
        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();

        // Show button Add
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < ObjectsToAdd.Length; i++)
            {
                var item = Object.Instantiate(ObjectsToAdd[i]);
                item.name = ObjectsToAdd[i].name;
                AssetDatabase.AddObjectToAsset(item, AssetObject);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(item));
            }
            AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
        }

        // Show button Delete
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Delete"))
        {
            for (int i = 0; i < ObjectsToAdd.Length; i++)
            {
                Object.DestroyImmediate(ObjectsToAdd[i], true);
            }
            AssetDatabase.SaveAssets();
        }
    }

}