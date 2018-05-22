using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Game.Editor
{
    public class DebugOptionsWindow : MonoBehaviour
    {
        private static Rect windowRect = new Rect(20, 20, 120, 50);

        private static bool isDisplayed;
        private static bool isLoaded;

        public static bool displayFloorPoint = true;
        public static bool displayHeightRay = true;
        public static bool displayLocationPoint = true;

        public static bool displayFloorColliders = true;

        public static bool displayPathFindingGrid = true;
        public static bool displayPathFindingConnections = true;
        public static bool displayPathFindingNodeId = true;

        public static bool displayTriggerZones = true;
        public static bool displaySpeechTriggers = true;
        public static bool displayFightTriggers = true;
        public static bool displayEndLevelTriggers = true;

        /*public static bool displayObstacleHeight = true;
        public static bool displayLowObstaclePoint = true;
        public static bool displayObstacleCollider = true;
        public static bool displayObstacleTopCollider = true;
        public static bool displayObstacleGroundCollider = true;*/

        public static bool displayEnemySpawn = true;
        public static bool displayFightSpawnLinks = true;

        [MenuItem("Level Editor/Debug Window")]
        public static void ToggleWindow()
        {
            if(!isLoaded)
                LoadPreferences();

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

            EditorGUILayout.LabelField("Scene Object", EditorUtilities.boldCenteredStyle);
            displayFloorPoint = EditorGUILayout.Toggle("Floor Point", displayFloorPoint);
            displayHeightRay = EditorGUILayout.Toggle("Height Ray", displayHeightRay);
            displayLocationPoint = EditorGUILayout.Toggle("Location Point", displayLocationPoint);
            EditorGUILayout.Space();

            /*EditorGUILayout.LabelField("Obstacle Scene Object", EditorUtilities.boldCenteredStyle);
            displayObstacleHeight = EditorGUILayout.Toggle("Obstacle Height", displayObstacleHeight);
            displayLowObstaclePoint = EditorGUILayout.Toggle("Low Obstacle Point", displayLowObstaclePoint);
            displayObstacleCollider = EditorGUILayout.Toggle("Obstacle Collider", displayObstacleCollider);
            displayObstacleTopCollider = EditorGUILayout.Toggle("Obstacle Top Collider", displayObstacleTopCollider);
            displayObstacleGroundCollider = EditorGUILayout.Toggle("Obstacle Ground Collider", displayObstacleGroundCollider);
            EditorGUILayout.Space();*/

            EditorGUILayout.LabelField("Floor", EditorUtilities.boldCenteredStyle);
            displayFloorColliders = EditorGUILayout.Toggle("Floor Colliders", displayFloorColliders);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("PathFinding", EditorUtilities.boldCenteredStyle);
            displayPathFindingGrid = EditorGUILayout.Toggle("Grid", displayPathFindingGrid);
            displayPathFindingConnections = EditorGUILayout.Toggle("Connections", displayPathFindingConnections);
            displayPathFindingNodeId = EditorGUILayout.Toggle("Node Id", displayPathFindingNodeId);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Triggers", EditorUtilities.boldCenteredStyle);
            displayTriggerZones = EditorGUILayout.Toggle("Triggers Zones", displayTriggerZones);
            displaySpeechTriggers = EditorGUILayout.Toggle("Speech", displaySpeechTriggers);
            displayFightTriggers = EditorGUILayout.Toggle("Fight", displayFightTriggers);
            displayEndLevelTriggers = EditorGUILayout.Toggle("End Level", displayEndLevelTriggers);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Spawns", EditorUtilities.boldCenteredStyle);
            displayEnemySpawn = EditorGUILayout.Toggle("Enemy Spawn", displayEnemySpawn);
            displayFightSpawnLinks = EditorGUILayout.Toggle("Fight Spawn Links", displayFightSpawnLinks);
            EditorGUILayout.Space();

            if (EditorGUI.EndChangeCheck())
            {
                SavePreferences();
                SceneView.RepaintAll();
            }

            GUI.DragWindow();
        }

        [InitializeOnLoadMethod]
        [DidReloadScripts]
        private static void LoadPreferences()
        {
            foreach (FieldInfo field_info in typeof(DebugOptionsWindow).GetFields())
            {
                if (field_info.FieldType.Name == "Boolean")
                {
                    if(EditorPrefs.HasKey(field_info.Name))
                        field_info.SetValue(null, EditorPrefs.GetBool(field_info.Name, true));
                }
            }

            isLoaded = true;
        }

        private static void SavePreferences()
        {
            foreach (FieldInfo field_info in typeof(DebugOptionsWindow).GetFields())
            {
                if (field_info.FieldType.Name == "Boolean")
                {
                    EditorPrefs.SetBool(field_info.Name, (bool)field_info.GetValue(null));
                }
            }
        }
    }
}
