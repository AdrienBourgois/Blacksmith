using UnityEngine;

namespace Game.Scripts.Navigation
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Floor : MonoBehaviour
    {
        public PathFinding PathFinding { get; private set; }

        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Floor");
            PathFinding = GetComponent<PathFinding>();
        }
    }
}
