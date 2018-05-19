using Game.Scripts;
using Game.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.SceneObjects
{
    [CustomEditor(typeof(SceneObject), true)]
    public class SceneObjectEditor : UnityEditor.Editor
    {
        private SceneObject selection;

        private static Rect windowRect = new Rect(20, 20, 250, 50);

        protected virtual void OnEnable()
        {
            selection = (SceneObject)target;
            Tools.hidden = true;
        }

        protected virtual void OnDisable()
        {
            selection = null;
            Tools.hidden = false;
        }

        protected virtual void OnSceneGUI()
        {
            DisplayMoveTool();
            HandleFunction();
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            windowRect = GUILayout.Window(10, windowRect, WindowFunction, "SceneObject");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            EditorGUI.BeginChangeCheck();
            selection.location = EditorGUILayout.Vector3Field("Location :", selection.location);
            if(EditorGUI.EndChangeCheck())
                selection.SetUnityPosition();

            if (GUILayout.Button("To Floor"))
                selection.ToFloor();
            GUILayout.Toggle(selection.IsOnFloorSpace(), "Is On Floor ?");
            GUI.DragWindow();
        }

        [DrawGizmo((GizmoType) 36, typeof(SceneObject))]
        protected static void DrawGizmo(SceneObject _scene_object, GizmoType _type)
        {
            Vector3 location = _scene_object.location;

            if(DebugOptionsWindow.displayHeightRay)
            {
                Handles.color = Color.blue;
                Handles.DrawLine(location.ToUnitySpace(), location.ToFloor().ToUnitySpace());
            }

            if (DebugOptionsWindow.displayFloorPoint)
            {
                Handles.color = Color.red;
                Handles.DrawWireDisc(location.ToFloor().ToUnitySpace(), Vector3.forward, 0.15f);
            }

            if (DebugOptionsWindow.displayLocationPoint)
            {
                Handles.color = Color.red;
                Handles.DrawSolidDisc(location.ToUnitySpace(), Vector3.forward, 0.1f);
            }
        }

        private void DisplayMoveTool()
        {
            float handle_size = HandleUtility.GetHandleSize(selection.location);
            const float snap = 0.1f;

            Vector3 location = selection.transform.position;

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.red;
            Vector3 x_handle_position = Handles.Slider(location, Vector3.right, handle_size, Handles.ArrowHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selection, "Change Scene Object Position");
                selection.transform.position = x_handle_position;
                selection.SetGamePosition();
            }

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.blue;
            Vector3 z_handle_position = Handles.Slider(location, GamePhysic.zAxis, handle_size, Handles.ArrowHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selection, "Change Scene Object Position");
                selection.transform.position = z_handle_position;
                selection.SetGamePosition();
            }

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.green;
            Vector3 y_handle_position = Handles.Slider(location, Vector3.up, handle_size, Handles.ArrowHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selection, "Change Scene Object Position");
                selection.transform.position = y_handle_position;
                selection.SetGamePosition();
            }

            EditorGUI.BeginChangeCheck();
            Handles.color = Color.magenta;
            Vector3 free_move_handle_position = Handles.Slider2D(location, Vector3.back, GamePhysic.zAxis, Vector3.right, handle_size / 4f, Handles.RectangleHandleCap, snap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selection, "Change Scene Object Position");
                selection.transform.position = free_move_handle_position;
                selection.SetGamePosition();
            }
        }
    }
}