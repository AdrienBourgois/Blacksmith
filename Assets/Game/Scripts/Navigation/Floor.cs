using UnityEngine;

namespace Game.Scripts.Navigation
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Floor : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Floor");
        }
    }
}
