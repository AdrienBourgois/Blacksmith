using Game.Scripts;
using Game.Scripts.Interfaces;
using Game.Scripts.SceneObjects;
using Game.Scripts.Triggers;
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

            GUILayout.Label("Scene Objects");

            if(GUILayout.Button("SceneObject"))
                CreateSceneObject<SceneObject>();
            if (GUILayout.Button("SpriteSceneObject"))
                CreateSceneObject<SpriteSceneObject>();
            if (GUILayout.Button("PhysicSceneObject"))
                CreateSceneObject<PhysicSceneObject>();
            if (GUILayout.Button("ObstacleSceneObject"))
                CreateObstacle();

            GUILayout.Label("Triggers");

            if (GUILayout.Button("BubbleSpeechTrigger"))
                CreateTrigger<BubbleSpeechTrigger>();

            GUI.DragWindow();
        }

        private static void CreateSceneObject<T>() where T : SceneObject
        {
            GameObject go = new GameObject(typeof(T).Name);
            T component = go.AddComponent<T>();
            Vector3 center_camera = new Vector3(Screen.width / 2f, Screen.height / 2f, SceneView.lastActiveSceneView.camera.nearClipPlane);
            Vector3 game_space = SceneView.lastActiveSceneView.camera.ScreenToWorldPoint(center_camera).ToGameSpace();
            game_space.y = 0;
            component.location = game_space;
            component.SetUnityPosition();
            Selection.objects = new Object[] {go};
        }

        private static void CreateTrigger<T>() where T : MonoBehaviour, ITriggerAction
        {
            GameObject go = new GameObject("Trigger " + typeof(T).Name);
            go.AddComponent<LevelTrigger>();
            T component = go.AddComponent<T>();
            Vector3 center_camera = new Vector3(Screen.width / 2f, Screen.height / 2f, SceneView.lastActiveSceneView.camera.nearClipPlane);
            Vector3 game_space = SceneView.lastActiveSceneView.camera.ScreenToWorldPoint(center_camera);
            game_space.z = 0;
            component.transform.position = game_space;
            Selection.objects = new Object[] { go };
        }

        private static void CreateObstacle()
        {
            GameObject obstacle_go = new GameObject("Obstacle");
            ObstacleSceneObject obstacle = obstacle_go.AddComponent<ObstacleSceneObject>();
            obstacle_go.AddComponent<PolygonCollider2D>();
            obstacle_go.layer = LayerMask.NameToLayer("Obstacle");

            GameObject ground = new GameObject("Ground");
            ground.transform.parent = obstacle_go.transform;
            ground.AddComponent<PolygonCollider2D>();
            ground.layer = LayerMask.NameToLayer("GroundObstacle");

            GameObject top = new GameObject("Top");
            top.transform.parent = obstacle_go.transform;
            top.AddComponent<PolygonCollider2D>();
            top.layer = LayerMask.NameToLayer("TopObstacle");

            obstacle.BakeColliders();
        }
    }
}
