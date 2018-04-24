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

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.red;
            Vector3 x_handle_position = Handles.Slider(scene_object.location.ToUnitySpace(), Vector3.right, handle_size, Handles.ArrowHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(scene_object, "Change Scene Object Position");
                scene_object.location.x = x_handle_position.x;
                scene_object.SetPosition();
            }

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.green;
            Vector3 z_handle_position = Handles.Slider(scene_object.location.ToUnitySpace(), GamePhysic.zAxis, handle_size, Handles.ArrowHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(scene_object, "Change Scene Object Position");
                scene_object.location.z = z_handle_position.y;
                scene_object.SetPosition();
            }
        }
    }
}