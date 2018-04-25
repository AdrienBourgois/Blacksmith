using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class DebugOptionsWindow : EditorWindow
    {
        public static bool displayFloorPoint = true;
        public static bool displayHeightRay = true;

        [MenuItem("Debug/Options")]
        private static void ShowWindow()
        {
            GetWindow<DebugOptionsWindow>();
        }

        private void OnEnable()
        {
            titleContent = new GUIContent("Debug Options");
        }

        private void OnGUI()
        {
            displayFloorPoint = EditorGUILayout.Toggle("Display Floor Point", displayFloorPoint);
            displayHeightRay = EditorGUILayout.Toggle("Display Height Ray", displayHeightRay);
        }
    }
}
