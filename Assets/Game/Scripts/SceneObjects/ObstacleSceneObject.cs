using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class ObstacleSceneObject : SpriteSceneObject
    {
        [Header("Obstacle Scene Object")]
        public PolygonCollider2D groundCollider;
        public PolygonCollider2D objectCollider;
        public PolygonCollider2D topCollider;

        public float height;
        public int lowTopPointIndex = -1;

        public void BakeColliders()
        {
            objectCollider = GetComponent<PolygonCollider2D>();
            groundCollider = gameObject.transform.Find("Ground").GetComponent<PolygonCollider2D>();
            topCollider = gameObject.transform.Find("Top").GetComponent<PolygonCollider2D>();

            lowTopPointIndex = GetLowestPointOfCollider(topCollider);
        }

        public bool IsValid()
        {
            return groundCollider && objectCollider && topCollider && lowTopPointIndex != -1;
        }

        private static int GetLowestPointOfCollider(PolygonCollider2D _collider)
        {
            int index = -1;
            float y = float.MaxValue;

            for (int i = 0; i < _collider.points.Length; i++)
            {
                Vector2 point = _collider.points[i];
                if (!(point.y < y)) continue;
                y = point.y;
                index = i;
            }

            return index;
        }
    }
}
