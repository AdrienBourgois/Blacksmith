using Game.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.SceneObjects
{
    [CustomEditor(typeof(ForegroundSpriteObject), true)]
    public class ForegroundSpriteObjectEditor : UnityEditor.Editor
    {
        private ForegroundSpriteObject foregroundSpriteSelection;

        private static Rect spriteWindowRect = new Rect(20, 20, 250, 50);

        protected void OnEnable()
        {
            foregroundSpriteSelection = (ForegroundSpriteObject)target;
        }

        protected void OnDisable()
        {
            foregroundSpriteSelection = null;
        }

        protected void OnSceneGUI()
        {
            HandleFunction();
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            spriteWindowRect = GUILayout.Window(40, spriteWindowRect, WindowFunction, "ForegroundSpriteObject");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            foregroundSpriteSelection.Sprite = (Sprite)EditorGUILayout.ObjectField("Sprite : ", foregroundSpriteSelection.Sprite, typeof(Sprite), false);
            foregroundSpriteSelection.OrderInLayer = EditorGUILayout.IntField("Order : ", foregroundSpriteSelection.OrderInLayer);
            GUI.DragWindow();
        }

        [DrawGizmo(GizmoType.InSelectionHierarchy, typeof(ForegroundSpriteObject))]
        private static void DrawGizmo(ForegroundSpriteObject _object, GizmoType _type)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(Vector3.zero, Vector3.right * 1000f);
            Gizmos.DrawRay(Vector3.zero, Vector3.left * 1000f);
            _object.SetProperties();
        }
    }
}