using Game.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.SceneObjects
{
    [CustomEditor(typeof(BackgroundSpriteObject), true)]
    public class BackgroundSpriteObjectEditor : UnityEditor.Editor
    {
        private BackgroundSpriteObject backgroundSpriteSelection;

        private static Rect spriteWindowRect = new Rect(20, 20, 250, 50);

        protected void OnEnable()
        {
            backgroundSpriteSelection = (BackgroundSpriteObject)target;
        }

        protected void OnDisable()
        {
            backgroundSpriteSelection = null;
        }

        protected void OnSceneGUI()
        {
            HandleFunction();
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            spriteWindowRect = GUILayout.Window(40, spriteWindowRect, WindowFunction, "BackgroundSpriteObject");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            backgroundSpriteSelection.Sprite = (Sprite)EditorGUILayout.ObjectField("Sprite : ", backgroundSpriteSelection.Sprite, typeof(Sprite), false);
            backgroundSpriteSelection.OrderInLayer = EditorGUILayout.IntField("Order : ", backgroundSpriteSelection.OrderInLayer);
            GUI.DragWindow();
        }

        [DrawGizmo(GizmoType.InSelectionHierarchy, typeof(BackgroundSpriteObject))]
        private static void DrawGizmo(BackgroundSpriteObject _object, GizmoType _type)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(Vector3.zero, Vector3.right * 1000f);
            Gizmos.DrawRay(Vector3.zero, Vector3.left * 1000f);
        }
    }
}