using ArashGh.Anima2D.Definitions;
using UnityEditor;

namespace ArashGh.Anima2D.Editor
{
    public class Animation2DAsset
    {
        [MenuItem("Anima2D/Animation2D", priority = 1000)]
        public static void CreateAnimation2DInstance()
        {
            ScriptableObjectUtility.CreateScriptableObject<Animation2DDefinition>();
        }
    }
}
