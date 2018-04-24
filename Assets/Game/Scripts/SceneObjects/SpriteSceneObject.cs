using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSceneObject : SceneObject
    {
        [Header("Sprite Scene Object")]
        [SerializeField] protected Sprite sprite;

        protected virtual void Start()
        {
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        }

        protected override void Update()
        {
            base.Update();
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

        protected override void UpdateRuntimeDebug()
        {
            base.UpdateRuntimeDebug();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }
    }
}