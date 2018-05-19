﻿using System.Reflection;
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

        public static bool displayTriggerZones = true;
        public static bool displaySpeechTriggers = true;

        public static bool displayObstacleHeight = true;
        public static bool displayLowObstaclePoint = true;
        public static bool displayObstacleCollider = true;
        public static bool displayObstacleTopCollider = true;
        public static bool displayObstacleGroundCollider = true;

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

            EditorGUILayout.PrefixLabel("Scene Object");
            displayFloorPoint = EditorGUILayout.Toggle("Display Floor Point", displayFloorPoint);
            displayHeightRay = EditorGUILayout.Toggle("Display Height Ray", displayHeightRay);
            displayLocationPoint = EditorGUILayout.Toggle("Display Location Point", displayLocationPoint);

            EditorGUILayout.PrefixLabel("Obstacle Scene Object");
            displayObstacleHeight = EditorGUILayout.Toggle("Display Obstacle Height", displayObstacleHeight);
            displayLowObstaclePoint = EditorGUILayout.Toggle("Display Low Obstacle Point", displayLowObstaclePoint);
            displayObstacleCollider = EditorGUILayout.Toggle("Display Obstacle Collider", displayObstacleCollider);
            displayObstacleTopCollider = EditorGUILayout.Toggle("Display Obstacle Top Collider", displayObstacleTopCollider);
            displayObstacleGroundCollider = EditorGUILayout.Toggle("Display Obstacle Ground Collider", displayObstacleGroundCollider);

            EditorGUILayout.PrefixLabel("Floor");
            displayFloorColliders = EditorGUILayout.Toggle("Display Floor Colliders", displayFloorColliders);

            EditorGUILayout.PrefixLabel("PathFinding");
            displayPathFindingGrid = EditorGUILayout.Toggle("Display Grid", displayPathFindingGrid);
            displayPathFindingConnections = EditorGUILayout.Toggle("Display Connections", displayPathFindingConnections);

            EditorGUILayout.PrefixLabel("Triggers");
            displayTriggerZones = EditorGUILayout.Toggle("Display Triggers Zones", displayTriggerZones);
            displaySpeechTriggers = EditorGUILayout.Toggle("Display Speech Triggers", displaySpeechTriggers);

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
