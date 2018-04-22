using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSceneObject : SceneObject
    {
        [Header("Sprite Scene Object")]
        [SerializeField] protected Sprite sprite;

        [SerializeField] protected float mFloorHeight;
        protected float mSpriteLowerBound;
        protected float mSpriteHalfWidth;

        protected virtual void Start()
        {
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
            mSpriteLowerBound = sprite_renderer.sprite.pivot.y;
            mSpriteHalfWidth = sprite_renderer.bounds.size.x * 0.5f;
        }

        protected override void Update()
        {
            base.Update();
        }

        protected virtual void OnDrawGizmos()
        {
            Vector3 floor_height_pos = new Vector3(transform.position.x, transform.position.y - mSpriteLowerBound + mFloorHeight, transform.position.z);
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(floor_height_pos + Vector3.left * mSpriteHalfWidth, floor_height_pos + Vector3.right * mSpriteHalfWidth);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            SetGraphicProperties();
        }

        protected void SetGraphicProperties()
        {
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();

            sprite_renderer.sprite = sprite;
            sprite_renderer.sortingLayerName = "Default";
        }
    }
}