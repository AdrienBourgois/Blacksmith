using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Entity;
using UnityEngine.Assertions;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Revive", menuName = "Attacks/Revive")]
    public class SoReviveAttack : SoBaseAttack
    {
        public override void Attack(BaseEntity _entity)
        {
            PlayerEntity p_entity = _entity as PlayerEntity;
            Assert.IsNotNull(p_entity, "[SoReviveAttack.Attack()] The variable p_entity is NULL");
            p_entity.RevivePlayer();
        }
    }
}