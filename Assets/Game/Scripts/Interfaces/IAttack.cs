using Game.Scripts.Entity;
using Game.Scripts.ScriptableObjects;

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
