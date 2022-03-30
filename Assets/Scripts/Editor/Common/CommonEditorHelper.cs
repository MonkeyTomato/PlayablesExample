using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CommonEditorHelper
{
    public static GUIStyle headerStyle
    {
        get
        {
            return new GUIStyle("AM HeaderStyle");
        }
    }

    public static void SaveEditorModify(UnityEngine.Object obj = null, SerializedObject serializedObject = null)
    {
        if (obj != null)
            EditorUtility.SetDirty(obj);
        if (serializedObject != null)
            serializedObject.ApplyModifiedProperties();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static List<T> GetAllObjectsOnlyInScene<T>() where T : Component
    {
        List<T> objectsInScene = new List<T>();

        foreach (T t in Resources.FindObjectsOfTypeAll(typeof(T)) as T[])
        {
            if (!EditorUtility.IsPersistent(t.transform.root.gameObject) && !(t.hideFlags == HideFlags.NotEditable || t.hideFlags == HideFlags.HideAndDontSave))
                objectsInScene.Add(t);
        }

        return objectsInScene;
    }

    public static int idCount = 0;
    public static string GetUniqueId()
    {
        idCount++;
        return string.Format("{0}{1}{2}{3}{4}{5}{6}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, idCount);
    }
}

