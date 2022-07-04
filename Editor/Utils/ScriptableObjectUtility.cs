using ArashGh.Anima2D.Definitions;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ArashGh.Anima2D.Editor
{
    public static class ScriptableObjectUtility
    {
        public static void CreateScriptableObject<T>() where T : DefinitionBase
        {
            T asset = ScriptableObject.CreateInstance<T>();

            MethodInfo getActiveFolderPath = typeof(ProjectWindowUtil).GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);

            string folderPath = (string)getActiveFolderPath.Invoke(null, null);
            string path = AssetDatabase.GenerateUniqueAssetPath(folderPath + $"/New {typeof(T).Name}.asset");

            ProjectWindowUtil.CreateAsset(asset, path);
        }
    }
}
