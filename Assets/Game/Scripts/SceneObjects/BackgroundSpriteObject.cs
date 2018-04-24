using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundSpriteObject : MonoBehaviour
    {
        [SerializeField] private int orderInLayer;
        [SerializeField] private Sprite sprite;

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
            sprite_renderer.sortingLayerName = "Background";
            sprite_renderer.sortingOrder = orderInLayer;

            Vector3 position = gameObject.transform.position;
            position.z = 10;
            gameObject.transform.position = position;
        }
    }
}