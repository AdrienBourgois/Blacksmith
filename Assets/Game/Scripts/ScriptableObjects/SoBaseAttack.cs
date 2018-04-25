using Game.Scripts.Entity;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class SoBaseAttack : ScriptableObject, Interfaces.IAttack
    {
        [SerializeField] protected float damages;
        [SerializeField] protected float coolDown;

        protected AttackEntity myAttackEntity;

        protected bool isAttacking;
        protected bool isInCooldwnl;

        protected bool isPlayer;

        public enum EAttackType
        {
            CAC,
            DISTANCE
        }

        protected EAttackType eAttackType;

        public virtual void Init(AttackEntity _my_attack_entity)
        {
            myAttackEntity = _my_attack_entity;
            isPlayer = (PlayerEntity) myAttackEntity != null ? true : false;
        }

        public virtual void LightGroundedAttack() { }

        public virtual void HeavyGroundedAttack() { }

        public virtual void StartCooldown() { }

        public void DamageEntity(BaseEntity _entity)
        {
            _entity.GetComponent<IDamagable>().ReceiveDamages(damages);
        }

        public EAttackType GetAttackType()
        {
            return eAttackType;
        }
        
        public bool IsAttacking()
        {
            return isAttacking;
        }
    }
}
