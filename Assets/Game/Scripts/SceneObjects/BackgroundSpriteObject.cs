using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundSpriteObject : MonoBehaviour
    {
        [Header("Background Scene Object")]
        [SerializeField]
        private int orderInLayer;

        [SerializeField]
        private Sprite sprite;

        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                sprite = value;
                GetComponent<SpriteRenderer>().sprite = sprite;
                SetProperties();
            }
        }

        public int OrderInLayer
        {
            get { return orderInLayer; }
            set
            {
                orderInLayer = value;
                SetProperties();
            }
        }

        private void Awake()
        {
            SetProperties();
        }

        private void OnValidate()
        {
            SetProperties();
        }

        public void SetProperties()
        {
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();

            sprite_renderer.sprite = sprite;
            sprite_renderer.sortingLayerName = "Background";
            sprite_renderer.sortingOrder = orderInLayer;
            sprite_renderer.drawMode = SpriteDrawMode.Sliced;

            Vector3 position = gameObject.transform.position;
            position.z = 10;
            gameObject.transform.position = position;

            if(transform.localScale != Vector3.one)
                Rescale();
        }

        private void Rescale()
        {
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
            Vector3 scale = transform.localScale;

            Vector2 size = sprite_renderer.size;
            size.x = size.x * scale.x;
            size.y = size.y * scale.y;
            sprite_renderer.size = size;

            transform.localScale = Vector3.one;
        }
    }
}