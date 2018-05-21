using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ForegroundSpriteObject : MonoBehaviour
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
            }
        }

        public int OrderInLayer
        {
            get { return orderInLayer; }
            set { orderInLayer = value; }
        }

        private void Awake()
        {
            SetProperties();
        }

        private void OnValidate()
        {
            SetProperties();
        }

        private void SetProperties()
        {
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();

            sprite_renderer.sprite = sprite;
            sprite_renderer.sortingLayerName = "Foreground";
            sprite_renderer.sortingOrder = OrderInLayer;
            sprite_renderer.drawMode = SpriteDrawMode.Sliced;

            Vector3 position = gameObject.transform.position;
            position.z = -10;
            gameObject.transform.position = position;
        }
    }
}