using Game.Scripts.Entity;

namespace Game.Scripts.Collectibles
{
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
