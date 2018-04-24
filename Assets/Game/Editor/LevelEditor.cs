using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class LevelEditor : EditorWindow
    {
        public Rect toolsWindowRect = new Rect(100, 100, 200, 200);

        [MenuItem("Level Editor/Editor Window")]
        public static void ShowWindow()
        {
            LevelEditor window = GetWindow<LevelEditor>("Level Editor", true, typeof(SceneView));
            window.Show();
            window.maximized = true;
        }

        private void OnGUI()
        {
            BeginWindows();

            toolsWindowRect = GUILayout.Window(1, toolsWindowRect, ToolsWindowLayout, "Tools");

            EndWindows();
        }

        private void SceneViewWindowsLayout(int _id)
        {

        }

        private void ToolsWindowLayout(int _id)
        {
            GUILayout.Button("Hi");
            GUI.DragWindow();
        }

        private void OnFocus()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGui;
            SceneView.onSceneGUIDelegate += OnSceneGui;
            Debug.Log("OnFocus");
        }

        private void OnSceneGui(SceneView _scene_view)
        {
            Handles.BeginGUI();
            Handles.DrawCamera(position, Camera.main, DrawCameraMode.Textured);
            Handles.EndGUI();
            Debug.Log("OnSceneGui");
        }
    }
}