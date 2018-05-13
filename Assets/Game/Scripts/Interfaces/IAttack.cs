using Game.Scripts.Entity;

namespace Game.Scripts.Interfaces
{
    public interface IAttack
    {
        void LightGroundedAttack();

        void HeavyGroundedAttack();

        bool CanAttack();

        void StartCooldown(float _cooldown);

        void DamageEntity(BaseEntity _entity, float _damages);
    }
}
