using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class AttackEntity : BaseEntity
    {
        [SerializeField]
        protected SoBaseAttack soAttack;

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
    }
}
