using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class DebugOptionsWindow : MonoBehaviour
    {
        private static Rect windowRect = new Rect(20, 20, 120, 50);

        private static bool isDisplayed;

        public static bool displayFloorPoint = true;
        public static bool displayHeightRay = true;
        public static bool displayFloorColliders = true;

        [MenuItem("Level Editor/Debug Window")]
        public static void ToggleWindow()
        {
            if(isDisplayed)
                SceneView.onSceneGUIDelegate -= OnScene;
            else
                SceneView.onSceneGUIDelegate += OnScene;
            isDisplayed = !isDisplayed;
            SceneView.RepaintAll();
        }

        private static void OnScene(SceneView _sceneview)
        {
            Handles.BeginGUI();
            windowRect = GUILayout.Window(0, windowRect, WindowFunction, "Debug Options");

            Handles.EndGUI();
        }

        private static void WindowFunction(int _id)
        {
            EditorGUI.BeginChangeCheck();
            displayFloorPoint = EditorGUILayout.Toggle("Display Floor Point", displayFloorPoint);
            displayHeightRay = EditorGUILayout.Toggle("Display Height Ray", displayHeightRay);
            displayFloorColliders = EditorGUILayout.Toggle("Display Floor Colliders", displayFloorColliders);
            if (EditorGUI.EndChangeCheck())
                SceneView.RepaintAll();
            GUI.DragWindow();
        }
    }
}
