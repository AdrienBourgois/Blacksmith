using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Interfaces
{
    public interface IAttack
    {
        void Init(AttackEntity my_attack_entity);

        void LightGroundedAttack();

        void HeavyGroundedAttack();

        void StartCooldown();

        bool IsAttacking();

        SO_BaseAttack.EAttackType GetAttackType();
    }
}
