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

        [NonSerialized]
        public Node[] connectedNodes;

        public int id;

        public int[] connectedNodesId;

        private NodeInfo info;
        public NodeInfo Info
        {
            get { return info ?? (info = new NodeInfo()); }
        }

        public Node(int _id, float _x, float _y)
        {
            id = _id;
            location = new Vector2(_x, _y);
        }
    }

    public class NodeInfo
    {
        public Node cameFrom;
        public float distance;
    }

    public class PathFinding : MonoBehaviour, ISerializationCallbackReceiver
    {
        public List<Node> nodes = new List<Node>();

        public bool isGridInitialized;
        public bool isConnectionsInitialized;

        public float gridInterval = 1.0f;
        public float connectionsPrecision = 1.5f;

        public bool WeightedFindPath(Vector2 _from, Vector2 _to, out List<Vector2> _path)
        {
            Node node_from = GetClosestNode(_from);
            Node node_to = GetClosestNode(_to);

            Stack<Node> open_list = new Stack<Node>();
            List<Node> closed_list = new List<Node>();

            open_list.Push(node_from);

            while (open_list.Count > 0)
            {
                Node current = open_list.Pop();
                if (current == node_to)
                {
                    _path = WeightedReconstruct(current, _from, _to);
                    foreach (Node node in nodes)
                    {
                        node.Info.cameFrom = null;
                        node.Info.distance = 0f;
                    }
                    return true;
                }

                foreach (Node connected_node in current.connectedNodes)
                {
                    if (open_list.Contains(connected_node) || closed_list.Contains(connected_node))
                        continue;

                    connected_node.Info.distance = current.Info.distance + Vector2.Distance(current.location, connected_node.location);
                    connected_node.Info.cameFrom = current;
                    open_list.Push(connected_node);
                }

                closed_list.Add(current);
                open_list = new Stack<Node>(open_list.OrderByDescending(_node => _node.Info.distance));
            }
            _path = null;
            return false;
        }

        private static List<Vector2> WeightedReconstruct(Node _last_node, Vector2 _from, Vector2 _to)
        {
            List<Vector2> path = new List<Vector2> { _to };

            Node current = _last_node;
            while (current.Info.cameFrom != null)
            {
                path.Add(current.location);
                current = current.Info.cameFrom;
            }

            path.Add(current.location);
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
                node.connectedNodes = GetNodesInRadius(node, connectionsPrecision).ToArray();

            isConnectionsInitialized = true;
        }

        private IEnumerable<Node> GetNodesInRadius(Node _node, float _radius)
        {
            List<Node> result_nodes = new List<Node>();

            foreach (Node node in nodes)
                if(node.id != _node.id && (node.location - _node.location).magnitude < _radius)
                    result_nodes.Add(node);

            return result_nodes;
        }

        private Node GetClosestNode(Vector2 _location)
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

            return closest_node;
        }

        public void OnBeforeSerialize()
        {
            foreach (Node node in nodes)
            {
                if(node.connectedNodes != null)
                    node.connectedNodesId = node.connectedNodes.Select(_node => _node.id).ToArray();
            }
        }

        public void OnAfterDeserialize()
        {
            foreach (Node node in nodes)
            {
                if (node.connectedNodesId != null)
                    node.connectedNodes = nodes.Where(_node => node.connectedNodesId.Contains(_node.id)).ToArray();
            }
        }

        public void Reset()
        {
            isConnectionsInitialized = false;
            isGridInitialized = false;
            nodes.Clear();
        }
    }
}
