using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Navigation
{
    [Serializable]
    public class Node
    {
        public Vector2 location;
        public int[] connectedNodes;
        public int id;

        public Node(int _id, float _x, float _y)
        {
            id = _id;
            location = new Vector2(_x, _y);
        }
    }

    public class PathFinding : MonoBehaviour
    {
        public List<Node> nodes = new List<Node>();

        public bool isGridInitialized;
        public bool isConnectionsInitialized;

        public float gridInterval = 1f;
        public float connectionsPrecision = 1.5f;

        public bool FindPath(Vector2 _from, Vector2 _to, out List<Vector2> _path)
        {
            int node_from = GetClosestNode(_from);
            int node_to = GetClosestNode(_to);
            Stack<int> open_list = new Stack<int>();
            open_list.Push(node_from);
            List<int> close_list = new List<int>();
            Dictionary<int, int> came_from = new Dictionary<int, int>();

            while (open_list.Count != 0)
            {
                int current = open_list.Pop();
                if (!close_list.Contains(current))
                {
                    if (current == node_to)
                    {
                        _path = Reconstruct(came_from, node_from, node_to, _from, _to);
                        return true;
                    }

                    foreach (int connected_node_id in nodes[current].connectedNodes)
                    {
                        if (!open_list.Contains(connected_node_id) && !close_list.Contains(connected_node_id))
                        {
                            open_list.Push(connected_node_id);
                            came_from[connected_node_id] = current;
                        }

                    }

                    close_list.Add(current);
                }
            }

            _path = null;
            return false;
        }

        private List<Vector2> Reconstruct(IDictionary<int, int> _nodes, int _node_from, int _node_to, Vector2 _from, Vector2 _to)
        {
            List<Vector2> path = new List<Vector2> {_to};

            int previous = _node_to;

            while (previous != _node_from)
            {
                previous = _nodes[previous];
                path.Add(nodes[previous].location);
            }

            path.Add(_from);
            path.Reverse();
            return path;
        }

        public void CreateGrid()
        {
            isConnectionsInitialized = false;
            nodes.Clear();

            int current_id = 0;

            PolygonCollider2D polygon = GetComponent<PolygonCollider2D>();
            Bounds bounds = polygon.bounds;

            for (float x = bounds.min.x + 0.01f; x < bounds.max.x; x += gridInterval)
            {
                for (float y = bounds.min.y + 0.01f; y < bounds.max.y; y += gridInterval)
                {
                    if(polygon.OverlapPoint(new Vector2(x, y)))
                    {
                        nodes.Add(new Node(current_id++, x, y));
                    }
                }
            }

            isGridInitialized = true;
        }

        public void CreateConnections()
        {
            foreach (Node node in nodes)
                node.connectedNodes = GetNodesInRadius(node, connectionsPrecision).Select(_node => _node.id).ToArray();

            isConnectionsInitialized = true;
        }

        private List<Node> GetNodesInRadius(Node _node, float _radius)
        {
            List<Node> result_nodes = new List<Node>();

            foreach (Node node in nodes)
                if(node.id != _node.id && (node.location - _node.location).magnitude < _radius)
                    result_nodes.Add(node);

            return result_nodes;
        }

        private Node GetClosestNode(Node _node)
        {
            Node closest_node = _node;
            float closest_distance = float.MaxValue;
            foreach (Node node in nodes)
            {
                if (node == _node)
                    continue;

                float distance = (node.location - _node.location).magnitude;

                if (!(distance < closest_distance))
                    continue;

                closest_distance = distance;
                closest_node = node;
            }

            return closest_node;
        }

        private int GetClosestNode(Vector2 _location)
        {
            Node closest_node = nodes[0];
            float closest_distance = float.MaxValue;
            foreach (Node node in nodes)
            {
                if (node.location == _location)
                    continue;

                float distance = (node.location - _location).magnitude;

                if (!(distance < closest_distance))
                    continue;

                closest_distance = distance;
                closest_node = node;
            }

            return closest_node.id;
        }
    }
}
