using Game.Scripts.AttackBehavior;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class SoBaseAttack : ScriptableObject
    {
        [SerializeField] protected float coolDown;

        /*erializeField] protected float damages;
        [SerializeField] protected EComboEffect comboEffect = EComboEffect.NONE;
        [SerializeField] protected float horizontalVelocity;
        [SerializeField] protected float verticalVelocity;*/

        [SerializeField] protected BaseBehavior.HitData hitData;

        [System.Serializable]
        public enum EComboEffect
        {
            NONE,
            VERTICAL_LAUNCH,
            HORIZONTAL_LAUNCH
        }

        public virtual void Attack(BaseEntity _entity)
        {
            _entity.StartCooldown(coolDown);
        }
    }
}
