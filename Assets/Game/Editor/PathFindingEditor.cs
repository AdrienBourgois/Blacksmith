using System.Collections.Generic;
using Game.Scripts.Navigation;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(PathFinding))]
    public class PathFindingEditor : UnityEditor.Editor
    {
        private PathFinding selection;

        private static Rect windowRect = new Rect(290, 20, 250, 50);

        private bool isTestingPathFinding;
        private Vector2 testPoint1;
        private Vector2 testPoint2;
        private List<Vector2> testPath;
        private bool isPathCorrect;

        private void OnEnable()
        {
            selection = (PathFinding)target;
            testPoint1 = selection.GetComponent<PolygonCollider2D>().points[0] + new Vector2(selection.transform.position.x, selection.transform.position.y);
            testPoint2 = testPoint1;
        }

        private void OnDisable()
        {
            selection = null;
        }

        protected void OnSceneGUI()
        {
            HandleFunction();

            if (isTestingPathFinding)
            {
                Handles.color = Color.cyan;
                Handles.DrawSolidDisc(testPoint1, Vector3.back, HandleUtility.GetHandleSize(testPoint1) * 0.1f);
                Handles.DrawSolidDisc(testPoint2, Vector3.back, HandleUtility.GetHandleSize(testPoint2) * 0.1f);
                if (isPathCorrect && testPath != null)
                {
                    for (int i = 0; i < testPath.Count - 1; i++)
                    {
                        Handles.color = Color.blue;
                        Handles.DrawLine(testPath[i], testPath[i + 1]);
                    }
                }
            }
        }

        private void HandleFunction()
        {
            Handles.BeginGUI();

            windowRect = GUILayout.Window(21, windowRect, WindowFunction, "PathFinding");

            Handles.EndGUI();
        }

        private void WindowFunction(int _id)
        {
            EditorGUI.BeginChangeCheck();

            if (GUILayout.Button("Reset"))
                selection.Reset();

            GUILayout.BeginHorizontal();
            selection.gridInterval = EditorGUILayout.FloatField("Interval", selection.gridInterval);
            if (GUILayout.Button("Create Grid"))
                selection.CreateGrid();
            GUILayout.EndHorizontal();

            if (selection.isGridInitialized)
            {
                GUILayout.BeginHorizontal();
                selection.connectionsPrecision = EditorGUILayout.FloatField("Precision", selection.connectionsPrecision);
                if (GUILayout.Button("Create Connections"))
                    selection.CreateConnections();
                GUILayout.EndHorizontal();
            }

            if (selection.isGridInitialized && selection.isConnectionsInitialized)
            {
                isTestingPathFinding = EditorGUILayout.BeginToggleGroup("Test Path", isTestingPathFinding);
                GUILayout.BeginHorizontal();
                testPoint1 = EditorGUILayout.Vector2Field("From :", testPoint1);
                testPoint2 = EditorGUILayout.Vector2Field("To :", testPoint2);
                if (GUILayout.Button("Create"))
                    isPathCorrect = selection.WeightedFindPath(testPoint1, testPoint2, out testPath);
                GUILayout.EndHorizontal();
                EditorGUILayout.EndToggleGroup();
            }

            GUI.DragWindow();
        }

        [DrawGizmo(GizmoType.InSelectionHierarchy, typeof(PathFinding))]
        protected static void DrawGizmo(PathFinding _path_finding, GizmoType _type)
        {
            if (DebugOptionsWindow.displayPathFindingGrid)
            {
                if (_path_finding.isGridInitialized)
                {

                    foreach (Node node in _path_finding.nodes)
                    {
                        Handles.color = Color.blue;
                        Handles.DrawSolidDisc(node.location, Vector3.back, 0.03f);
                    }
                }
            }

            if (DebugOptionsWindow.displayPathFindingConnections)
            {
                if (_path_finding.isConnectionsInitialized)
                {
                    foreach (Node node in _path_finding.nodes)
                    {
                        Handles.color = Color.red;
                        foreach (Node connected_node in node.connectedNodes)
                        {
                            Handles.DrawLine(node.location, connected_node.location);
                        }
                    }
                }
            }
        }
    }
}
