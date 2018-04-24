using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class SO_BaseAttack : ScriptableObject, Game.Scripts.Interfaces.IAttack
    {
        [SerializeField] protected float damages;
        [SerializeField] protected float coolDown;

        protected AttackEntity myAttackEntity;

        protected bool isAttacking;
        protected bool isInCooldwnl;

        public enum EAttackType
        {
            CAC,
            DISTANCE
        }

        protected EAttackType eAttackType;

        public virtual void Init(AttackEntity _my_attack_entity)
        {
            myAttackEntity = _my_attack_entity;
        }

        public virtual void LightGroundedAttack() { }

        public virtual void HeavyGroundedAttack() { }

        public virtual void StartCooldown() { }

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
