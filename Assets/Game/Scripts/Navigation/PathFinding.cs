using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Navigation
{
    public class Node
    {
        public Vector2 location;
        public Node[] connectedNodes;

        public Node(float _x, float _y)
        {
            location = new Vector2(_x, _y);
        }
    }

    public class PathFinding : MonoBehaviour
    {
        private readonly List<Node> nodes = new List<Node>();
        private bool isConstructed;

        public List<Vector2> FindPath(Vector2 _from, Vector2 _to)
        {
            Node node_from = GetClosestNode(_from);
            Node node_to = GetClosestNode(_to);
            Stack<Node> open_list = new Stack<Node>(nodes);
            List<Node> close_list = new List<Node>();
            Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();

            while (open_list.Count != 0)
            {
                Node current = open_list.Pop();
                if (!close_list.Contains(current))
                {
                    if (current == node_to)
                    {
                        came_from[node_to] = current;
                        return Reconstruct(came_from, node_from, node_to, _from, _to);
                    }

                    foreach (Node connected_node in current.connectedNodes)
                    {
                        if (!open_list.Contains(connected_node) && !close_list.Contains(connected_node))
                        {
                            open_list.Push(connected_node);
                            came_from[connected_node] = current;
                        }
                    }
                    close_list.Add(current);
                }
            }

            return null;
        }

        private List<Vector2> Reconstruct(IDictionary<Node, Node> _nodes, Node _node_from, Node _node_to, Vector2 _from, Vector2 _to)
        {
            List<Vector2> path = new List<Vector2> {_to};

            Node previous = _node_to;

            while (previous != _node_from)
            {
                previous = _nodes[previous];
                path.Add(previous.location);
            }

            path.Add(_from);
            path.Reverse();
            return path;
        }

        public void CreateGrid()
        {
            nodes.Clear();

            for (int i = 0; i < 10f; i++)
            {
                for (int j = 0; j < 10f; j++)
                {
                    nodes.Add(new Node(i + (j%2 == 0 ? 0.5f : 0f), j));
                }
            }

            CreateConnections(1.5f);
            isConstructed = true;
        }

        private void CreateConnections(float _precision)
        {
            foreach (Node node in nodes)
            {
                node.connectedNodes = GetNodesInRadius(node, _precision).ToArray();
            }
        }

        private List<Node> GetNodesInRadius(Node _node, float _radius)
        {
            List<Node> result_nodes = new List<Node>();

            foreach (Node node in nodes)
            {
                if((node.location - _node.location).magnitude < _radius)
                    result_nodes.Add(node);
            }

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

        public Vector2 point1;
        public Vector2 point2;

        private void OnDrawGizmos()
        {
            ContactFilter2D filter = new ContactFilter2D();
            int layer_mask = LayerMask.GetMask("Floor");
            filter.SetLayerMask(layer_mask);
            RaycastHit2D[] results = new RaycastHit2D[10];
            RaycastHit2D hit = Physics2D.Linecast(point1, point2, layer_mask);
            if (hit)
            {
                Debug.DrawLine(point1, point2, Color.green);
                Handles.color = Color.blue;
                Handles.DrawSolidDisc(hit.point, Vector3.back, 0.1f);
            }
            else
                Debug.DrawLine(point1, point2, Color.red);

            if (!isConstructed)
                return;

            foreach (Node node in nodes)
            {
                Handles.color = Color.green;
                foreach (Node connected_node in node.connectedNodes)
                    Handles.DrawLine(node.location, connected_node.location);

                Handles.color = Color.blue;
                Handles.DrawSolidDisc(node.location, Vector3.back, 0.1f);
            }
            List<Vector2> result = FindPath(nodes[0].location, nodes[1].location);

            for (int i = 0; i < result.Count - 1; i++)
            {
                Handles.color = Color.red;
                Handles.DrawLine(result[i],  result[i+1]);
            }
        }
    }
}
