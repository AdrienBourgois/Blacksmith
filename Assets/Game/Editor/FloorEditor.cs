﻿using Game.Scripts.Navigation;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(Floor))]
    public class FloorEditor : UnityEditor.Editor
    {
        private Floor selection;

        private static Rect windowRect = new Rect(20, 20, 250, 50);

        private void OnEnable()
        {
            selection = (Floor)target;
        }

        private void OnDisable()
        {
            selection = null;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            selection.gameObject.layer = LayerMask.NameToLayer("Floor");
        }

        protected void OnSceneGUI()
        {
            HandleFunction();
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            windowRect = GUILayout.Window(20, windowRect, WindowFunction, "Floor");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            EditorGUI.BeginChangeCheck();

            if (GUILayout.Button("Merge Colliders"))
                MergeColliders();
            GUI.DragWindow();
        }

        [DrawGizmo((GizmoType) 34, typeof(Floor))]
        private static void DrawColliders(Floor _floor, GizmoType _type)
        {
            if(DebugOptionsWindow.displayFloorColliders)
                foreach (PolygonCollider2D polygon in _floor.GetComponents<PolygonCollider2D>())
                    DrawCollider(polygon);
        }

        private static void DrawCollider(PolygonCollider2D _collider)
        {
            Vector2 offset = _collider.offset + new Vector2(_collider.gameObject.transform.position.x, _collider.gameObject.transform.position.y);
            for (int i = 0; i < _collider.points.Length - 1; i++)
            {
                Handles.color = Color.red;
                Handles.DrawLine(_collider.points[i] + offset, _collider.points[i + 1] + offset);
            }
            Handles.DrawLine(_collider.points[0] + offset, _collider.points[_collider.points.Length-1] + offset);
        }

        private void MergeColliders()
        {
            Undo.RegisterCompleteObjectUndo(selection.gameObject, "Merge Colliders");

            CompositeCollider2D composite = Undo.AddComponent<CompositeCollider2D>(selection.gameObject);
            composite.generationType = CompositeCollider2D.GenerationType.Manual;
            composite.geometryType = CompositeCollider2D.GeometryType.Polygons;

            PolygonCollider2D[] old_polygons = selection.gameObject.GetComponents<PolygonCollider2D>();

            foreach (PolygonCollider2D collider in old_polygons)
                collider.usedByComposite = true;

            composite.GenerateGeometry();

            for (int i = 0; i < composite.pathCount; i++)
            {
                PolygonCollider2D collider = Undo.AddComponent<PolygonCollider2D>(selection.gameObject);
                Vector2[] points = new Vector2[composite.GetPathPointCount(i)];
                composite.GetPath(i, points);
                collider.points = points;
            }

            foreach (PolygonCollider2D collider in old_polygons)
                Undo.DestroyObjectImmediate(collider);

            Undo.DestroyObjectImmediate(composite);
            Undo.DestroyObjectImmediate(selection.gameObject.GetComponent<Rigidbody2D>());
        }
    }
}