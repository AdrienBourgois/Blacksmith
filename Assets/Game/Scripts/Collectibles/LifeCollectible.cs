using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.Collectibles
{
    [CreateAssetMenu(fileName = "LifeCollectible", menuName = "Collectibles/Life", order = 2)]
    public class LifeCollectible : Collectible
    {
        public float lifeGained;

        public override void Collect(PlayerEntity _player)
        {
            base.Collect(_player);
            _player.Health += lifeGained;
        }
    }
}
