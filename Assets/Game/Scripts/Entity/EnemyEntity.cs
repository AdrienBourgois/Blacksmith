using Game.Scripts.AI;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class EnemyEntity : BaseEntity
    {
        private EnemyBehavior behavior;

        [SerializeField] private float attackDistance;

        #region Unity Methods

        protected override void Start()
        {
            base.Start();

            behavior = new EnemyBehavior(this, attackDistance);
        }

        protected override void Update()
        {
            base.Update();

            behavior.Update();
        }

        #endregion
    }
}