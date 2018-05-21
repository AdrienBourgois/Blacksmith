using Game.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.SceneObjects
{
    [CustomEditor(typeof(SpriteSceneObject), true)]
    public class SpriteSceneObjectEditor : SceneObjectEditor
    {
        private SpriteSceneObject spriteSelection;

        private static Rect spriteWindowRect = new Rect(290, 20, 250, 50);

        protected override void OnEnable()
        {
            base.OnEnable();
            spriteSelection = (SpriteSceneObject) target;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            spriteSelection = null;
        }

        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
            HandleFunction();
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            spriteWindowRect = GUILayout.Window(11, spriteWindowRect, WindowFunction, "SpriteSceneObject");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            spriteSelection.Sprite = (Sprite)EditorGUILayout.ObjectField(spriteSelection.Sprite, typeof(Sprite), false);
            GUI.DragWindow();
        }
    }
}