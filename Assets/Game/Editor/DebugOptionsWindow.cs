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

        public static bool displayPathFindingGrid = true;
        public static bool displayPathFindingConnections = true;

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

        private static void OnScene(SceneView _scene_view)
        {
            Handles.BeginGUI();
            windowRect = GUILayout.Window(0, windowRect, WindowFunction, "Debug Options");

            Handles.EndGUI();
        }

        private static void WindowFunction(int _id)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PrefixLabel("Scene Object");
            displayFloorPoint = EditorGUILayout.Toggle("Display Floor Point", displayFloorPoint);
            displayHeightRay = EditorGUILayout.Toggle("Display Height Ray", displayHeightRay);
            displayFloorColliders = EditorGUILayout.Toggle("Display Floor Colliders", displayFloorColliders);

            EditorGUILayout.PrefixLabel("PathFinding");
            displayPathFindingGrid = EditorGUILayout.Toggle("Display Grid", displayPathFindingGrid);
            displayPathFindingConnections = EditorGUILayout.Toggle("Display Connections", displayPathFindingConnections);

            if (EditorGUI.EndChangeCheck())
                SceneView.RepaintAll();
            GUI.DragWindow();
        }
    }
}
