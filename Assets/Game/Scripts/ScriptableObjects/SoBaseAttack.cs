using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    public class SoBaseAttack : ScriptableObject
    {
        [SerializeField] protected float damages;
        [SerializeField] protected float coolDown;
        [SerializeField] private luch lunchAntiono;

        [System.Serializable]
        public enum luch
        {
            Antiono_pd,
            Antiono_gay
        }

        public virtual void Attack(BaseEntity _entity)
        {
            _entity.StartCooldown(coolDown);
        }
    }
}
