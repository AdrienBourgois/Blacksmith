﻿using Game.Scripts.Entity;
using Game.Scripts.ScriptableObjects;

namespace Game.Scripts.Interfaces
{
    public interface IAttack
    {
        void Init(AttackEntity _my_attack_entity);

        void LightGroundedAttack();

        void HeavyGroundedAttack();

        void StartCooldown();

        bool IsAttacking();

        SoBaseAttack.EAttackType GetAttackType();
    }
}
