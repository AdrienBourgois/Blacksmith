using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class SoBaseAttack : ScriptableObject
    {
        [SerializeField] protected float damages;
        [SerializeField] protected float coolDown;
        [SerializeField] protected EComboEffect comboEffect = EComboEffect.NONE;
        [SerializeField] protected float horizontalVelocity;
        [SerializeField] protected float verticalVelocity;

        [System.Serializable]
        public enum EComboEffect
        {
            NONE,
            VERTICAL_LAUNCH,
            HORIZONTAL_LAUNCH
        }

        public struct HitData
        {
            public EComboEffect comboEffect;

            public float damages;
            public float xDir;
            public float xVelocity;
            public float yVelocity;
        }

        public virtual void Attack(BaseEntity _entity)
        {
            _entity.StartCooldown(coolDown);
        }
    }
}
