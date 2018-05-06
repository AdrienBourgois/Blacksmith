using Game.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(PhysicSceneObject), true)]
    public class PhysicSceneObjectEditor : SpriteSceneObjectEditor
    {
        private PhysicSceneObject physicSelection;

        private static Rect physicWindowRect = new Rect(560, 20, 250, 50);

        protected override void OnEnable()
        {
            base.OnEnable();
            physicSelection = (PhysicSceneObject)target;
            Tools.hidden = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            physicSelection = null;
            Tools.hidden = false;
        }

        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
            HandleFunction();
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            physicWindowRect = GUILayout.Window(12, physicWindowRect, WindowFunction, "PhysicSceneObject");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            physicSelection.weight = EditorGUILayout.FloatField("Weight : ", physicSelection.weight);
            physicSelection.friction = EditorGUILayout.FloatField("Friction : ", physicSelection.friction);
            GUI.DragWindow();
        }

    }
}