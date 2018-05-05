using Game.Scripts.Entity;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class SoBaseAttack : ScriptableObject
    {
        [SerializeField] protected float damages;
        [SerializeField] protected float coolDown;

        public virtual void Attack(BaseEntity _entity)
        {
            _entity.StartCooldown(coolDown);
        }
    }
}
