using Game.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.SceneObjects
{
    [CustomEditor(typeof(ObstacleSceneObject), true)]
    public class ObstacleSceneObjectEditor : SpriteSceneObjectEditor
    {
        private ObstacleSceneObject obstacleSelection;

        private static Rect physicWindowRect = new Rect(560, 20, 250, 50);

        protected override void OnEnable()
        {
            base.OnEnable();
            obstacleSelection = (ObstacleSceneObject)target;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            obstacleSelection = null;
        }

        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
            HandleFunction();
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            physicWindowRect = GUILayout.Window(15, physicWindowRect, WindowFunction, "ObstacleSceneObject");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            obstacleSelection.height= EditorGUILayout.FloatField("Height : ", obstacleSelection.height);
            if (GUILayout.Button("Bake"))
                obstacleSelection.BakeColliders();
            GUI.DragWindow();
        }

        [DrawGizmo((GizmoType)34, typeof(ObstacleSceneObject))]
        protected static void DrawGizmo(ObstacleSceneObject _obstacle, GizmoType _type)
        {
            if (!_obstacle.IsValid()) return;

            /*if (DebugOptionsWindow.displayObstacleCollider)
                EditorUtilities.DrawCollider(_obstacle.objectCollider, EditorUtilities.orange);
            if (DebugOptionsWindow.displayObstacleTopCollider)
                EditorUtilities.DrawCollider(_obstacle.topCollider, EditorUtilities.blueMiku);
            if (DebugOptionsWindow.displayObstacleGroundCollider)
                EditorUtilities.DrawCollider(_obstacle.groundCollider, EditorUtilities.darkGreen);*/

        }

    }
}