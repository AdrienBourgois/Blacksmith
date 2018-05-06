using Game.Scripts.Navigation;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(PathFinding))]
    public class PathFindingEditor : UnityEditor.Editor
    {
        private PathFinding selection;

        private static Rect windowRect = new Rect(290, 20, 250, 50);

        private void OnEnable()
        {
            selection = (PathFinding)target;
        }

        private void OnDisable()
        {
            selection = null;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        protected void OnSceneGUI()
        {
            HandleFunction();
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            windowRect = GUILayout.Window(21, windowRect, WindowFunction, "PathFinding");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            EditorGUI.BeginChangeCheck();

            if (GUILayout.Button("Create PathFinding"))
                selection.CreateGrid();
            GUI.DragWindow();
        }

    }
}
