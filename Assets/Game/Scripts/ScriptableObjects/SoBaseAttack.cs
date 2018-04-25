using Game.Scripts.Entity;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class SoBaseAttack : ScriptableObject, Interfaces.IAttack
    {
        [SerializeField] protected float weakDamages;
        [SerializeField] protected float heavyDamages;

        [SerializeField] protected float weakCoolDown;
        [SerializeField] protected float heavyCoolDown;

        protected AttackEntity myAttackEntity;

        protected bool isAttacking;
        protected bool isInCooldown;

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
            isPlayer = myAttackEntity as PlayerEntity != null ? true : false;

            isAttacking = false;
            isInCooldown = false;
        }

        public virtual void LightGroundedAttack() { }

        public virtual void HeavyGroundedAttack() { }

        public virtual void StartCooldown(float _cooldown) { }

        public void DamageEntity(BaseEntity _entity, float _dammages)
        {
            _entity.GetComponent<IDamagable>().ReceiveDamages(_dammages);
        }

        public EAttackType GetAttackType()
        {
            return eAttackType;
        }
        
        public bool CanAttack()
        {
            return !isAttacking && !isInCooldown;
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }
    }
}
