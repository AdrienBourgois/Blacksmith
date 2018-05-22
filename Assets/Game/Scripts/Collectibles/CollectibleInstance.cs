using Game.Scripts.Collectibles;
using Game.Scripts.Entity;
using Game.Scripts.SceneObjects;
using UnityEngine;

namespace Game.Scripts.Sounds
{
    public class CollectibleInstance : PhysicSceneObject
    {
        private Collectible collectible;

        public static CollectibleInstance Create(Vector3 _location, Collectible _collectible)
        {
            GameObject go = new GameObject();
            CollectibleInstance instance = go.AddComponent<CollectibleInstance>();
            instance.location = _location;
            instance.collectible = _collectible;
            //instance.velocity = new Vector3(Random.Range(-1f, 1f), 0.3f, Random.Range(-1f, 1f));
            instance.weight = 0.15f;

            SpriteRenderer sprite_renderer = go.GetComponent<SpriteRenderer>();
            sprite_renderer.sprite = instance.collectible.sprite;

            return instance;
        }

        public void Collect(PlayerEntity _player)
        {
            collectible.Collect(_player);
        }
    }
}
