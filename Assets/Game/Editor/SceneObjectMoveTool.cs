using Game.Scripts;
using Game.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(SceneObject), true)]
    public class SceneObjectMoveTool : UnityEditor.Editor
    {
        private void OnEnable()
        {
            Tools.hidden = true;
        }

        private void OnDisable()
        {
            Tools.hidden = false;
        }

        protected void OnSceneGUI()
        {
            SceneObject scene_object = (SceneObject) target;

            float handle_size = HandleUtility.GetHandleSize(scene_object.location);
            const float snap = 0.1f;

            Vector3 location = scene_object.transform.position;

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.red;
            Vector3 x_handle_position = Handles.Slider(location, Vector3.right, handle_size, Handles.ArrowHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(scene_object, "Change Scene Object Position");
                scene_object.transform.position = x_handle_position;
                scene_object.SetGamePosition();
            }

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.blue;
            Vector3 z_handle_position = Handles.Slider(location, GamePhysic.zAxis, handle_size, Handles.ArrowHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(scene_object, "Change Scene Object Position");
                scene_object.transform.position = z_handle_position;
                scene_object.SetGamePosition();
            }

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.green;
            Vector3 y_handle_position = Handles.Slider(location, Vector3.up, handle_size, Handles.ArrowHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(scene_object, "Change Scene Object Position");
                scene_object.transform.position = y_handle_position;
                scene_object.SetGamePosition();
            }
        }
    }
}