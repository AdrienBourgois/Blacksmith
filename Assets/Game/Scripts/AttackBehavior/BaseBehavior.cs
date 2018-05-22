using System.Collections;
using System.Collections.Generic;
using Game.Scripts.AttackBehavior;
using Game.Scripts.Entity;
using Game.Scripts.SceneObjects;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.AttackBehavior
{
    public class BaseBehavior : SpriteSceneObject
    {
        public delegate void TriggerHandler(BaseEntity _entity, HitData _data);
        public TriggerHandler onEntityHit;

        [HideInInspector] public HitData hitData;
        
        [System.Serializable]
        public struct HitData
        {
            public SoBaseAttack.EComboEffect comboEffect;

            public float damages;
            [HideInInspector] public float xDir;
            public float xVelocity;
            public float yVelocity;
        }
    }
}
