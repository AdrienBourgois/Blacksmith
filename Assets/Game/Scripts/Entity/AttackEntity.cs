using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interfaces;
using Game.Scripts.SceneObjects;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class AttackEntity : BaseEntity
    {
        [SerializeField]
        protected SO_BaseAttack soAttack;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            soAttack.Init(this);
        }

        protected override void ListenXAxis(float _value)
        {
            if (!soAttack.IsAttacking())
                base.ListenXAxis(_value);

        }

        protected override void ListenZAxis(float _value)
        {
            if (!soAttack.IsAttacking())
                base.ListenZAxis(_value);
        }

        protected override void Jump()
        {
            if (!soAttack.IsAttacking())
                base.Jump();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}
