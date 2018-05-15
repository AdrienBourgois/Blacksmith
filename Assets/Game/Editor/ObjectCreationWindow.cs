using Game.Scripts;
using Game.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class ObjectCreationWindow : EditorWindow
    {
        private static Rect windowRect = new Rect(20, 20, 120, 50);

        private static bool isDisplayed;

        [MenuItem("Level Editor/Object Creation Window")]
        public static void ToggleWindow()
        {
            if (isDisplayed)
                SceneView.onSceneGUIDelegate -= OnScene;
            else
                SceneView.onSceneGUIDelegate += OnScene;
            isDisplayed = !isDisplayed;
            SceneView.RepaintAll();
        }

        private static void OnScene(SceneView _scene_view)
        {
            Handles.BeginGUI();
            windowRect = GUILayout.Window(1, windowRect, WindowFunction, "Object Creation");

            Handles.EndGUI();
        }

        private static void WindowFunction(int _id)
        {
            GUILayout.Label("Create :");
            if(GUILayout.Button("SceneObject"))
                CreateObject<SceneObject>();
            if (GUILayout.Button("SpriteSceneObject"))
                CreateObject<SpriteSceneObject>();
            if (GUILayout.Button("PhysicSceneObject"))
                CreateObject<PhysicSceneObject>();

            GUI.DragWindow();
        }

        private static void CreateObject<T>() where T : SceneObject
        {
            GameObject go = new GameObject(typeof(T).Name);
            T component = go.AddComponent<T>();
            Vector3 center_camera = new Vector3(Screen.width / 2f, Screen.height / 2f, SceneView.lastActiveSceneView.camera.nearClipPlane);
            Vector3 game_space = SceneView.lastActiveSceneView.camera.ScreenToWorldPoint(center_camera).ToGameSpace();
            game_space.y = 0;
            component.location = game_space;
            Selection.objects = new Object[] {go};
        }
    }
}
