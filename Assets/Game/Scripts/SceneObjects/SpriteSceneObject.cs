using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSceneObject : SceneObject
    {
        [Header("Sprite Scene Object")]
        [SerializeField]
        private Sprite sprite;

        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                sprite = value;
                GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }

        protected virtual void OnValidate()
        {
            SetGraphicProperties();
        }

        protected void SetGraphicProperties()
        {
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();

            if (sprite_renderer.sprite)
                Sprite = sprite_renderer.sprite;
            sprite_renderer.sortingLayerName = "Default";
        }
    }
}