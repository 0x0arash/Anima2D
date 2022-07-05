using ArashGh.Anima2D.Definitions;
using System.IO;
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

            var typeName = typeof(T).Name;
            var path = EditorUtility.SaveFilePanel($"Save New {typeName}", "Assets", $"New {typeName}", "anima2d.asset");

            AssetDatabase.CreateAsset(asset, "Assets/" + Path.GetRelativePath("Assets", path));
        }
    }
}
