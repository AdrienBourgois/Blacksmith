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

        protected virtual void ListenXAxis(float _value)
        {
            if (!soAttack.IsAttacking())
                TryMove(Vector3.right * _value);

        }

        protected virtual void ListenZAxis(float _value)
        {
            if (!soAttack.IsAttacking())
                TryMove(Vector3.up * _value);
        }

        protected override void Jump()
        {
            if (!soAttack.IsAttacking())
                base.Jump();
        }
    }
}
